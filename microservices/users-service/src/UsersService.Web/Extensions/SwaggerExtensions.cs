using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace UsersService.Web.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument(document =>
        {
            document.Title = "UsersService.V1";
            document.DocumentName = "v1";
            document.Version = "v1";
            document.ApiGroupNames = ["v1"];
            document.OperationProcessors.Add(new AddUserHeadersProcessor());
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
}

public class AddUserHeadersProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-User-Id",
            Kind = OpenApiParameterKind.Header,
            Type = NJsonSchema.JsonObjectType.String,
            Description = "User ID",
            IsRequired = false
        });

        context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-User-Role",
            Kind = OpenApiParameterKind.Header,
            Type = NJsonSchema.JsonObjectType.String,
            Description = "User role",
            IsRequired = false
        });

        return true;
    }
}
