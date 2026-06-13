using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;
using System.Text.Json;

namespace SweebAppFront.Services
{
    class AuthService: IAuthService
    {
        HttpClient _client = new HttpClient();
        JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase , WriteIndented = true };
        Uri uri = new Uri("https://localhost:7832/api/v1/User/Create");
        Uri deviceUri = new Uri("https://localhost:7832/api/v1/Device/create");

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {

            if(!string.IsNullOrWhiteSpace(request.Username) && request.Username.Length > 4)
            {
                try
                {
                    string json = JsonSerializer.Serialize(request.Username, _options);
                    StringContent content = new StringContent(json, encoding: System.Text.Encoding.UTF8, mediaType: "application/json");

                    var response = await _client.PostAsync(uri, content);
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var responseContent = JsonSerializer.Deserialize<ApiResponse<User>>(responseJson, _options);

                    if(!response.IsSuccessStatusCode || responseContent == null || responseContent.Data == null)
                    {
                        return new LoginResponse
                        {
                            Success = false,
                            Message = responseContent?.Message ?? "An error occurred while processing your request. Please try again later."
                        };
                    }

                    await CreateDevice(responseContent);

                    return new LoginResponse
                    {
                        Success = true,
                        Message = "Welcome !"
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Login failed: {ex.Message}");
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "An error occurred while processing your request. Please try again later."
                    };
                }
            }
            return new LoginResponse
            {
                Success = false,
                Message = "Please tell us your name. Make sure it's more than 4 characters long."
            };
        }

        private async Task CreateDevice(ApiResponse<User> response)
        {
            if (response.Data == null)
            {
                return;
            }

            var deviceInfo = new {
                UserId = response.Data.Id,
                DeviceName = DeviceInfo.Current.Name,
                DeviceOS = DeviceInfo.Current.Platform.ToString(),
            };

            string json = JsonSerializer.Serialize(deviceInfo, _options);
            StringContent content = new StringContent(json, encoding: System.Text.Encoding.UTF8, mediaType: "application/json");

            var deviceResponse = await _client.PostAsync(deviceUri, content);
            if (!deviceResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Device registration failed: {deviceResponse.StatusCode}");
            }
        }
    }
}
