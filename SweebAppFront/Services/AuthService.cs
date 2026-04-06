using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;

namespace SweebAppFront.Services
{
    class AuthService : IAuthService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            await Task.Delay(1200);

            if (request.Email == "admin@test.com" && request.Password == "1234")
            {
                return new LoginResponse
                {
                    Success = true,
                    Message = "Login successfully !",
                    Token = "fake-jwt-token",
                    Username = "Admin"
                };
            }

            return new LoginResponse
            {
                Success = false,
                Message = "Email or Password is incorrect !"
            };

        }
    }
}
