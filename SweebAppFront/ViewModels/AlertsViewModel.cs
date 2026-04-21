using SweebAppFront.Enums;
using SweebAppFront.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SweebAppFront.ViewModels
{
    public class AlertsViewModel
    {
        public ObservableCollection<Alerts> Alerts { get; set; } 

        public AlertsViewModel()
        {
            Alerts = new ObservableCollection<Alerts>
            {
                new Alerts
                {
                    IdAlert = 1,
                    Message = "Phishing attempt blocked",
                    Severity = AlertSeverity.Critical,
                    IsRead = false,
                    CreatedAt = DateTime.Now.AddMinutes(-2)
                },
                new Alerts
                {
                    IdAlert = 2,
                    Message = "Suspicious login detected",
                    Severity = AlertSeverity.Warning,
                    IsRead = false,
                    CreatedAt = DateTime.Now.AddMinutes(-10)
                },
                new Alerts
                {

                    IdAlert = 3,
                    Message = "No threats detected",
                    Severity = AlertSeverity.Info,
                    IsRead = true,
                    CreatedAt = DateTime.Now.AddHours(-1)
                }
            };
        }
    }
}
