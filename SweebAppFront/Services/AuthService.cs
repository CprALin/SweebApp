using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;

namespace SweebAppFront.Services
{
    class AuthService : IAuthService
    {
        /*
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
        */

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            await Task.Delay(1000);

            if(request.Username != string.Empty && request.Username.Length > 4)
            {
                return new LoginResponse
                {
                    Success = true,
                    Message = "Login successfully !"
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
