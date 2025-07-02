using UsersService.Web;
using UsersService.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilogWithDefaults();

builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddPresentation();

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new(1, 0))
    .ReportApiVersions()
    .Build();

var group = app.MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(versionSet)
    .HasApiVersion(new(1, 0));

group.MapGet("/ping", (HttpContext context) =>
{
    var userId = context.Request.Headers["X-User-Id"].FirstOrDefault() ?? "anonymous";
    var role = context.Request.Headers["X-User-Role"].FirstOrDefault() ?? "none";
    var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? "none";

    return Results.Ok(new
    {
        service = "UserService",
        message = "pong",
        correlationId,
        userId,
        role
    });
}).WithTags("Ping");

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.MapHealthChecks("api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLoggingWithDefaults();

app.UseExceptionHandler();

await app.RunAsync();
