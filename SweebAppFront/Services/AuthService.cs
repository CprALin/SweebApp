using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;

namespace SweebAppFront.Services
{
    class AuthService : IAuthService
    {

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            await Task.Delay(1000);

            if(request.Username != string.Empty && request.Username.Length > 4)
            {
                return new LoginResponse
                {
                    Success = true,
                    Message = "Welcome !"
                };
            }
            return new LoginResponse
            {
                Success = false,
                Message = "Please tell us your name. Make sure it’s more than 4 characters long."
            };
        }
    }
}
