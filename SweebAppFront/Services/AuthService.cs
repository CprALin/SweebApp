using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;
using System.Text.Json;

namespace SweebAppFront.Services
{
    class AuthService : IAuthService
    {
        HttpClient _client = new HttpClient();
        JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase , WriteIndented = true };
        Uri uri = new Uri("https://localhost:7832/api/v1/User/Create");
        Uri deviceUri = new Uri("https://localhost:7832/api/v1/Device/create");

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {

            if(request.Username != string.Empty && request.Username.Length > 4)
            {
                string json = JsonSerializer.Serialize(request.Username, _options);
                StringContent content = new StringContent(json, encoding: System.Text.Encoding.UTF8, mediaType: "application/json");

                var response = await _client.PostAsync(uri, content);
                var responseContent = JsonSerializer.Deserialize<ApiResponse<User>>(await response.Content.ReadAsStringAsync(), _options);

                if(responseContent == null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "An error occurred while processing your request. Please try again later."
                    };
                }
                
                await CreateDevice(responseContent);

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

        private async Task CreateDevice(ApiResponse<User> response)
        {
            var deviceInfo = new {
                UserId = response.Data.Id,
                DeviceName = DeviceInfo.Current.Name,
                DeviceOS = DeviceInfo.Current.Platform.ToString(),
            };

            string json = JsonSerializer.Serialize(deviceInfo, _options);
            StringContent content = new StringContent(json, encoding: System.Text.Encoding.UTF8, mediaType: "application/json");

            await _client.PostAsync(deviceUri, content);
        }
    }
}
