using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SweebAppFront.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IAuthStateService _authStateService;

        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isLoading;
        private bool _isLoggedIn;
        private string _username =string.Empty;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value); 
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthService authService,IAuthStateService authStateService)
        {
            _authService = authService;
            _authStateService = authStateService;
            LoginCommand = new Command(async () => await LoginAsync(), CanLogin);
        }

        private bool CanLogin()
        {
            return !IsLoading;
        }

        private async Task LoginAsync()
        {
            if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter both email and password.";
                return;
            }

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var request = new LoginRequest
                {
                    Email = Email,
                    Password = Password
                };

                var response = await _authService.LoginAsync(request);

                if (response.Success)
                {
                    _authStateService.IsLoggedIn = true;
                    _authStateService.Username = response.Username;

                    IsLoggedIn = true;
                    Username = response.Username;
                    ErrorMessage = string.Empty;
                }
                else
                {
                    IsLoading = false;
                    ErrorMessage = response.Message;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error has occured. Please try again.";
            }
            finally
            {
                IsLoading = false;
                (LoginCommand as Command)?.ChangeCanExecute();
            }
        }
    }

}
