namespace Vektorel.Api.Middlewares;

public class AccessCheckMiddleware
{
    private readonly RequestDelegate next;
    public AccessCheckMiddleware(RequestDelegate request)
    {
        next = request;
    }
    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("va-code"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Anahtarı Yok!");
            return;
        }

        var key = context.Request.Headers["va-code"];

        if (key != "123")
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Api Anahtarı Yanlış");
            return;
        }

        await next(context);
    }
}
