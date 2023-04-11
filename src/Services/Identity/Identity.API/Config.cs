namespace Identity.API;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    // TODO: move to settings file
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("api1", "My API")
            {
                Scopes = new List<string>()
                {
                    "api1"
                }
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // machine to machine client
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1" }
            },
                
            // interactive ASP.NET Core MVC client
            new Client
            {
                ClientId = "webClient",
                ClientSecrets = { new Secret("webClientSecret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                },

                AccessTokenLifetime = Convert.ToInt32(TimeSpan.FromHours(2).TotalSeconds),
            }
        };
}