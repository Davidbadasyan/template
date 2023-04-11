namespace Common.Application.Middlewares;

public class RequestHeadersToClaimsMiddleware
{
    private readonly RequestDelegate _next;
    public RequestHeadersToClaimsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value.Contains("/api/"))
        {
            var claims = new List<Claim>();

            if (context.Request.Headers.ContainsKey("UserId"))
            {
                claims.Add(new Claim("UserId", context.Request.Headers["UserId"]));
            }
            if (context.Request.Headers.ContainsKey("UserEmail"))
            {
                claims.Add(new Claim(ClaimTypes.Email, context.Request.Headers["UserEmail"]));
            }
            if (context.Request.Headers.ContainsKey("UserFirstName"))
            {
                claims.Add(new Claim(ClaimTypes.GivenName, WebUtility.UrlDecode(context.Request.Headers["UserFirstName"])));
            }
            if (context.Request.Headers.ContainsKey("UserLastName"))
            {
                claims.Add(new Claim(ClaimTypes.Surname, WebUtility.UrlDecode(context.Request.Headers["UserLastName"])));
            }
            if (context.Request.Headers.ContainsKey("UserOrganizationId"))
            {
                claims.Add(new Claim("UserOrganizationId", context.Request.Headers["UserOrganizationId"]));
            }

            context.User.AddIdentity(new ClaimsIdentity(claims));
            await _next(context);
        }
        else
        {
            await _next(context);
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class RequestHeadersToClaimsMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestHeadersToClaims(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestHeadersToClaimsMiddleware>();
    }
}