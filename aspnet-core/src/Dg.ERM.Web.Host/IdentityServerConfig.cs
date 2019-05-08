//using Castle.Core.Resource;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Dg.ERM.Web.Host
//{
//    public static class IdentityServerConfig
//    {
//        public static IEnumerable<IResource> GetApiResources()
//        {
//            return new List<ApiResource>
//        {
//            new ApiResource("default-api", "Default (all) API")
//        };
//        }

//        public static IEnumerable<IdentityResource> GetIdentityResources()
//        {
//            return new List<IdentityResource>
//        {
//            new IdentityResources.OpenId(),
//            new IdentityResources.Profile(),
//            new IdentityResources.Email(),
//            new IdentityResources.Phone()
//        };
//        }

//        public static IEnumerable<Client> GetClients()
//        {
//            return new List<Client>
//        {
//            new Client
//            {
//                ClientId = "client",
//                AllowedGrantTypes = GrantTypes.ClientCredentials.Union(GrantTypes.ResourceOwnerPassword).ToList(),
//                AllowedScopes = {"default-api"},
//                ClientSecrets =
//                {
//                    new Secret("secret".Sha256())
//                }
//            }
//        };
//        }
//    }

//}
