using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using OrderManagement.Api.Swagger;

namespace OrderManagement.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header usando el esquema Bearer.\n\n" +
                                  "Ejemplo: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<AuthorizeOperationFilter>();

            });

            return services;
        }
    }
}
