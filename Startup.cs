// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;
using pmonidentity.Domains.Services;
using pmonidentity.Domains.Utilities;
using pmonidentity.IdentityServer;
using pmonidentity.Repositories;
using pmonidentity.Services;
using pmonidentity.Utilities;

namespace pmonidentity {
	public class Startup {
		public IConfiguration Configuration { get; }
		public IHostingEnvironment Environment { get; }

		public Startup(IConfiguration configuration, IHostingEnvironment environment) {
			Configuration = configuration;
			Environment = environment;
		}

		public void ConfigureServices(IServiceCollection services) {
			// uncomment, if you want to add an MVC-based UI
			services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

			// register dbcontext
			services.AddDbContext<CtxPmonDb>(m => m.UseMySql(Configuration.GetConnectionString("PmonDb")));

			// dependency injection for repository
			services.AddScoped<IRepoMUser, RepoMUser>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			// dependency injection for service
			services.AddScoped<ISvsUser, SvsUser>();

			// dependency injection for utilities
			services.AddScoped<IUtlPasswordHasher, UtlPasswordHasher>();

			// dependency injection for resource owner password validator implementation of identity server
			services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
			services.AddTransient<IProfileService, ProfileService>();

			// cors
			services.AddCors(o => o.AddPolicy("PmonIdentityCorsPolicy", m => {
				m
					.AllowAnyOrigin()
					// .WithOrigins("http:// localhost:3000")
					.AllowAnyMethod()
					.AllowAnyHeader()
					// .AllowCredentials()
					.Build();
			}));

			// configure identity server
			var builder = services.AddIdentityServer(m => {
				m.Events.RaiseErrorEvents = true;
				m.Events.RaiseInformationEvents = true;
				m.Events.RaiseFailureEvents = true;
				m.Events.RaiseSuccessEvents = true;
			})
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApis())
				.AddInMemoryClients(Config.GetClients())
				.AddProfileService<ProfileService>();

			if (Environment.IsDevelopment()) {
				builder.AddDeveloperSigningCredential();
			}
			else {
				throw new Exception("need to configure key material");
			}
		}

		public void Configure(IApplicationBuilder app) {

			app.UseCors("PmonIdentityCorsPolicy");

			if (Environment.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// uncomment if you want to support static files
			// app.UseStaticFiles();
			// app.UseDefaultFiles();

			app.UseIdentityServer();

			// JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			// uncomment, if you want to add an MVC-based UI
			app.UseMvcWithDefaultRoute();
		}
	}
}
