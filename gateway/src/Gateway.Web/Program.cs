using Gateway.Web;
using Gateway.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("yarp.json", optional: false, reloadOnChange: true);

builder.Host.UseSerilogWithDefaults();

builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddPresentation();

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("YARP"));

var app = builder.Build();

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

app.MapReverseProxy();

await app.RunAsync();
