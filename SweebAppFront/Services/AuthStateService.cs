using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;
using SweebAppFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SweebAppFront.Services
{
    public class AuthStateService : BaseViewModel, IAuthStateService
    {
        private bool _isLoggedIn = false;
        private string _username = string.Empty;

        HttpClient _client = new HttpClient();
        JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        Uri uri = new Uri("https://localhost:7832/api/v1/User/get");

        public AuthStateService()
        {

            CheckLoginStatus();

        }

        public async void CheckLoginStatus()
        {
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var userInfo = JsonSerializer.Deserialize<ApiResponse<User>>(content, _options);

                    var user = userInfo?.Data;
                    var username = user?.UserName?.Trim() ?? string.Empty;

                    if (user != null && user.Id != 0 && !string.IsNullOrWhiteSpace(username))
                    {
                        Username = username;
                        IsLoggedIn = true;
                    }
                    else
                    {
                        ClearLoginStateIfNeeded();
                    }
                }
                else
                {
                    ClearLoginStateIfNeeded();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking login status: {ex.Message}");
                ClearLoginStateIfNeeded();
            }
        }

        private void ClearLoginStateIfNeeded()
        {
            if (IsLoggedIn)
                return;

            IsLoggedIn = false;
            Username = string.Empty;
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                if (_isLoggedIn == value) return;
                _isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (_username == value) return;
                _username = value;
                OnPropertyChanged();
            }
        }

    }
}
