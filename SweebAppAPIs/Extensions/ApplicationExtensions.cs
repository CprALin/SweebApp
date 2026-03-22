using SweebAppAPIs.Services;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Extensions
{
	public static class ApplicationExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IAuthService , AuthService>();
			services.AddScoped<IJwtService , JwtService>();
			services.AddScoped<IUserService , UserService>();
			services.AddScoped<IPasswordHashService , PasswordHashService>();
			return services;
		}
	}
}
