using SweebAppFront.Models;
using System.Collections.ObjectModel;


namespace SweebAppFront.ViewModels
{
    class ThreatsViewModel : BaseViewModel
    {
        public ObservableCollection<ThreatEvents> ThreatEvents { get; set; }

        private int _columns = 1;

        public int Columns {
            get => _columns;
            set { 
                if(_columns != value)
                {
                    _columns = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public ThreatsViewModel()
        {
            ThreatEvents = new ObservableCollection<ThreatEvents>
            {
                new ThreatEvents
                {

                    IdThreatEvent = 1,
                    URL = "https://secure-paypal-login.co/auth",
                    Protocol = "HTTPS",
                    Timestamp = new DateTime(2024, 3, 18, 9, 12, 44),
                    Verdict = "Phishing",
                    ActionTaken = "Blocked",
                    Score = 98,
                    Category = "Brand Impersonation",
                    DeviceId = 1
                },
            
                new ThreatEvents
                {
                    IdThreatEvent = 2,
                    URL = "https://accounts-google.support/login",
                    Protocol = "HTTPS",
                    Timestamp = new DateTime(2024, 3, 18, 9, 25, 10),
                    Verdict = "Phishing",
                    ActionTaken = "Warning",
                    Score = 95,
                    Category = "Credential Harvesting",
                    DeviceId = 1
                },
                new ThreatEvents
                {
                    IdThreatEvent = 3,
                    URL = "https://verify-microsoft-security.live/session",
                    Protocol = "HTTPS",
                    Timestamp = new DateTime(2024, 3, 18, 10, 1, 37),
                    Verdict = "Phishing",
                    ActionTaken = "Allowed",
                    Score = 97,
                    Category = "Fake Login Page",
                    DeviceId = 2
                },
                new ThreatEvents
                {
                    IdThreatEvent = 2,
                    URL = "https://accounts-google.support/login",
                    Protocol = "HTTPS",
                    Timestamp = new DateTime(2024, 3, 18, 9, 25, 10),
                    Verdict = "Phishing",
                    ActionTaken = "Warning",
                    Score = 95,
                    Category = "Credential Harvesting",
                    DeviceId = 1
                },
                new ThreatEvents
                {
                    IdThreatEvent = 3,
                    URL = "https://verify-microsoft-security.live/session",
                    Protocol = "HTTPS",
                    Timestamp = new DateTime(2024, 3, 18, 10, 1, 37),
                    Verdict = "Phishing",
                    ActionTaken = "Allowed",
                    Score = 97,
                    Category = "Fake Login Page",
                    DeviceId = 2
                }
            };
        }
    }
}
