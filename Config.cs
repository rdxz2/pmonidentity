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
				new ApiResource("pmonapi", "Pmon API")
			 };
		}

		public static IEnumerable<Client> GetClients() {
			return new Client[] {
				new Client{
					ClientId = "pmon",
					ClientName = "Pmon Web",
					AllowedGrantTypes = {
						GrantType.ClientCredentials,
						GrantType.ResourceOwnerPassword
					},
					RequireClientSecret = false,
					AlwaysIncludeUserClaimsInIdToken = true,

					RedirectUris = { "http://localhost:3000/home" },
					PostLogoutRedirectUris = { "http://localhost:3000/login" },
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