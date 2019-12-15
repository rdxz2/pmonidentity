// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace pmonidentity {
	public static class Config {
		public static IEnumerable<IdentityResource> GetIdentityResources() {
			return new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};
		}

		public static IEnumerable<ApiResource> GetApis() {
			return new ApiResource[] {
				// main pmon web api 
				new ApiResource("pmonapi", "Pmon Web API")
			 };
		}

		public static IEnumerable<Client> GetClients() {
			return new Client[] {
				// main pmon webapp (reactjs)
				new Client{
					ClientId = "pmon",
					ClientName = "Pmon Website",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					RequirePkce = true,
					RequireClientSecret = false,

					// token lifetime = 10 hours
					AccessTokenLifetime = 3600 * 10,

					RedirectUris = {  "http://localhost:3000/#/callback" },
					PostLogoutRedirectUris = { "http://localhost:3000/#/logout/callback" },
					AllowedCorsOrigins = { "http://localhost:3000" },

					AllowedScopes = {
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"pmonapi",
						"openid"
					}
				}
			};
		}
	}
}