namespace Gateway.Web.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument(document =>
        {
            document.Title = "Gateway.V1";
            document.DocumentName = "v1";
            document.Version = "v1";
            document.ApiGroupNames = ["v1"];

            document.PostProcess = doc =>
            {
                var usersOp = new OpenApiOperation
                {
                    Summary = "Ping UserService",
                    Tags = { "UserService" },
                    Responses =
                    {
                        ["200"] = new OpenApiResponse { Description = "OK" }
                    }
                };
                foreach (var header in GetDefaultHeaders())
                    usersOp.Parameters.Add(header);

                var metricsOp = new OpenApiOperation
                {
                    Summary = "Ping MetricsService",
                    Tags = { "MetricsService" },
                    Responses =
                    {
                        ["200"] = new OpenApiResponse { Description = "OK" }
                    }
                };
                foreach (var header in GetDefaultHeaders())
                    metricsOp.Parameters.Add(header);

                var reportsOp = new OpenApiOperation
                {
                    Summary = "Ping ReportsService",
                    Tags = { "ReportsService" },
                    Responses =
                    {
                        ["200"] = new OpenApiResponse { Description = "OK" }
                    }
                };
                foreach (var header in GetDefaultHeaders())
                    reportsOp.Parameters.Add(header);

                doc.Paths["/api/v1/users/ping"] = new OpenApiPathItem { { "get", usersOp } };
                doc.Paths["/api/v1/metrics/ping"] = new OpenApiPathItem { { "get", metricsOp } };
                doc.Paths["/api/v1/reports/ping"] = new OpenApiPathItem { { "get", reportsOp } };
            };
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithUi(this IApplicationBuilder app)
    {
        app.UseOpenApi(settings => settings.Path = "/api/specification.json");

        app.UseSwaggerUi(settings =>
        {
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });

        app.UseReDoc(settings =>
        {
            settings.Path = "/docs";
            settings.DocumentPath = "/api/specification.json";
        });

        return app;
    }

    private static IList<OpenApiParameter> GetDefaultHeaders() =>
    [
        new()
        {
            Name = "X-User-Id",
            Kind = OpenApiParameterKind.Header,
            Type = NJsonSchema.JsonObjectType.String,
            Description = "User ID",
            IsRequired = false
        },
        new()
        {
            Name = "X-User-Role",
            Kind = OpenApiParameterKind.Header,
            Type = NJsonSchema.JsonObjectType.String,
            Description = "User role",
            IsRequired = false
        },
        new()
        {
            Name = "X-Correlation-Id",
            Kind = OpenApiParameterKind.Header,
            Type = NJsonSchema.JsonObjectType.String,
            Description = "Correlation ID",
            IsRequired = false
        }
    ];
}
