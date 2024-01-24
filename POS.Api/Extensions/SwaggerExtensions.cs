using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "POS API",
                Version = "v1",
                Description = "Punto de Venta API 2022",
                TermsOfService = new Uri("https://opensource.org/licenses/NIT"),
                Contact = new OpenApiContact
                {
                    Name = "VLADHAT SRL",
                    Email = "vladhatsrl@gmail.com",
                    Url = new Uri("https://vladhat.com.pe")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://opensource.org/licenses/")
                }
            };

            services.AddSwaggerGen(x =>
            {
                openApi.Version = "v1";
                x.SwaggerDoc("v1", openApi);

                var securitySheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                x.AddSecurityDefinition(securitySheme.Reference.Id, securitySheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securitySheme, new string[]{ } }
                });
            });

            return services;
        }
    }
}
