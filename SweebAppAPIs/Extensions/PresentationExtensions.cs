using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

namespace SweebAppAPIs.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddOpenApi();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token like: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCors", p =>
                    p.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                );
            });

            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", jwtOptions =>
            {
                var metadataAddress = config["Api:MetadataAddress"];

                if (string.IsNullOrEmpty(metadataAddress))
                    throw new Exception("MetadataAddress missing in config");
                jwtOptions.MetadataAddress = metadataAddress;
                jwtOptions.Authority = config["Api:Authority"];
                jwtOptions.Audience = config["Api:Audience"];
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudiences = config.GetSection("Api:ValidAudiences").Get<string[]>(),
                    ValidIssuers = config.GetSection("Api:ValidIssuers").Get<string[]>()
                };

                jwtOptions.MapInboundClaims = false;
            });

            services.AddAuthorization();

            return services;
        }
    }
}
