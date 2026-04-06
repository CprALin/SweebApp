using SweebAppFront.Services.Interfaces;
using SweebAppFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Services
{
    public class AuthStateService : BaseViewModel, IAuthStateService
    {
        private bool _isLoggedIn = false;
        private string _username = string.Empty;

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
