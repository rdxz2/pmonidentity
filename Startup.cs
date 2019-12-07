// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pmonidentity.Domains.Models;
using pmonidentity.Domains.Repositories;
using pmonidentity.IdentityServer;
using pmonidentity.Repositories;

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
			//services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

			// cors
			services.AddCors(o => o.AddPolicy("PmonIdentityCorsPolicy", m => {
				m
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials()
					.Build();
			}));

			// jwt

			// identity server
			var builder = services.AddIdentityServer()
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

			// register dbcontext
			services.AddDbContext<CtxPmondb>(option => option.UseMySql(Configuration.GetConnectionString("Pmondb")));

			// dependency injection untuk repository
			services.AddScoped<IRepoMUser, RepoMUser>();

			// dependency injection untuk service

			// dependency injection untuk implementasi autentikasi identity server
			services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
			services.AddTransient<IProfileService, ProfileService>();
		}

		public void Configure(IApplicationBuilder app) {
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseCors("PmonIdentityCorsPolicy");

			if (Environment.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// uncomment if you want to support static files
			//app.UseStaticFiles();

			app.UseIdentityServer();

			// JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			// uncomment, if you want to add an MVC-based UI
			//app.UseMvcWithDefaultRoute();
		}
	}
}
