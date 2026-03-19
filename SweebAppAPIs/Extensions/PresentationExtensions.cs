using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;


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

                options.AddSecurityRequirement(document => new() { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });
               
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
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = config["Jwt:Audience"],
                    ValidIssuer = config["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(config["Jwt:Key"]!)    
                    )
                };

                jwtOptions.MapInboundClaims = false;
            });

            services.AddAuthorization();

            return services;
        }
    }
}
