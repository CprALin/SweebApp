using SweebAppFront.Enums;
using SweebAppFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class Alerts : BaseViewModel
    {
        private bool _isRead;

        public int IdAlert { get; set; }
        public string Message { get; set; } = string.Empty;
        public AlertSeverity Severity { get; set; }
        public bool IsRead 
        { 
          get => _isRead;
            set
            {
                SetProperty(ref _isRead, value);
                if (SetProperty(ref _isRead, value))
                    OnPropertyChanged(nameof(Opacity));
            }         
        }
        public double Opacity => IsRead ? 0.7 : 1.0;
        public DateTime CreatedAt { get; set; }
        public string TimeDisplay => CreatedAt.ToString("HH:mm");
    }
}
