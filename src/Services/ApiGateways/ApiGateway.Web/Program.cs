// ========================================= Add services to the container. ============================================ //

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Authorization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", reloadOnChange: true, optional: true)
    .AddJsonFile($"./OcelotConfig/configuration.{builder.Environment.EnvironmentName}.json", reloadOnChange: true, optional: true)
    .AddOcelot($"./OcelotConfig/{builder.Environment.EnvironmentName}", builder.Environment)
    .AddJsonFile($"ocelot.json", reloadOnChange: true, optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["Services:Identity"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
    });
});

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(b =>
        b.WithOrigins(builder.Configuration["WebSpaHostName"])
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials()));


// ========================================= Configure the HTTP request pipeline ============================================ //
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireAuthorization("ApiScope");
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

var ocelotConfig = new OcelotPipelineConfiguration
{
    AuthorizationMiddleware = async (ctx, next) =>
    {
        var userId = ctx.User.Claims.FirstOrDefault(c => c.Type.Equals("userId"))?.Value;
        var email = ctx.User.Claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;
        var firstName = ctx.User.Claims.FirstOrDefault(c => c.Type.Equals("firstName"))?.Value;
        var lastName = ctx.User.Claims.FirstOrDefault(c => c.Type.Equals("lastName"))?.Value;
        var role = ctx.User.Claims.FirstOrDefault(c => c.Type.Contains("role"))?.Value;

        var downstreamRequest = ctx.Items.DownstreamRequest();
        var downstreamRoute = ctx.Items.DownstreamRoute();
        var errors = ctx.Items.Errors();

        if (!CanDoCurrentAction(role, downstreamRoute.RouteClaimsRequirement))
        {
            ctx.Items.SetError(new UnauthorizedError("You don't have permission to complete this action"));
            return;
        }

        var templatePlaceholderNameAndValues = ctx.Items.TemplatePlaceholderNameAndValues();
        var companyId = templatePlaceholderNameAndValues.SingleOrDefault(t => t.Name == "{companyId}")?.Value;

        if (downstreamRequest.Method == "OPTIONS" || email == null)
        {
            await next.Invoke();
            return;
        }

        // DO NOT make http call to get user, instead add details to token and read it from claims

        /*var httpClientFactory = ctx.RequestServices.GetService<IHttpClientFactory>();
        var client = httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(30);
        var response = await client.GetAsync($"{builder.Configuration["Services:UserManagement"]}users?email={email}");
        await using var responseStream = await response.Content.ReadAsStreamAsync();
        var user = await System.Text.Json.JsonSerializer.DeserializeAsync<User>(responseStream);*/

        //TODO: make user permission checking (send also info to be checked)

        downstreamRequest.Headers.Add("UserId", userId);
        downstreamRequest.Headers.Add("UserEmail", email);
        downstreamRequest.Headers.Add("UserFirstName", firstName);
        downstreamRequest.Headers.Add("UserLastName", lastName);
        await next.Invoke();
    }
};

app.UseWebSockets();
await app.UseOcelot(ocelotConfig);

app.Run();



static bool CanDoCurrentAction(string userCurrentRole, Dictionary<string, string> routeClaimsRequirement)
{
    var requiredRole = routeClaimsRequirement.FirstOrDefault(r => r.Key.Equals("Role")).Value;

    if (string.IsNullOrWhiteSpace(requiredRole))
        return true;

    const string roleUser = "user";
    const string roleSuperAdmin = "superadmin";

    switch (requiredRole)
    {
        case roleUser:
            return userCurrentRole.Equals(roleUser) || userCurrentRole.Equals(roleSuperAdmin);
        case roleSuperAdmin:
            return userCurrentRole.Equals(roleSuperAdmin);
        default:
            throw new ArgumentException("Role specified incorrectly");
    }
}