using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace mvcWebFirstSolucation
{

    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>
            {
                new ApiResource("俺是测试客户端","My Api App")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "mvc",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    ClientName = "俺是测试客户端",
                    ClientSecrets ={
                        new Secret("secret".Sha256())
                    },
                    LogoUri = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1562435010217&di=189c5a1b3d010ee0ee046ba4f3267935&imgtype=0&src=http%3A%2F%2Fwww.kokojia.com%2FPublic%2Fimages%2Fupload%2Farticle%2F2016-12%2F5860df01eae7d.jpg",
                    RequireConsent = true,
                    RedirectUris = {"http://localhost:5001/signin-oidc",
                        "http://localhost:5002/signin-oidc" } ,
                    PostLogoutRedirectUris = {"http://localhost:5001/signout-callback-oidc" ,
                        "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId
                    }
                }
            };
        }
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser()
                {
                    SubjectId = "10000",
                    Username = "zara",
                    Password = "112233"
                }
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
