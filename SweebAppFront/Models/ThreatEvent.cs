using SweebAppAPIs.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SweebAppFront.Models
{
    public class ThreatEvent : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string URL { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Verdict { get; set; } = string.Empty;
        private ThreatStatus _actionTaken;
        public ThreatStatus ActionTaken 
        {
            get => _actionTaken;
            set
            {
                if(_actionTaken != value)
                {
                    _actionTaken = value;
                   OnPropertyChanged(nameof(ActionTaken));
                }
            }
        }
        public double Score { get; set; }
        public string Category { get; set; } = string.Empty;
        public int DeviceId { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}