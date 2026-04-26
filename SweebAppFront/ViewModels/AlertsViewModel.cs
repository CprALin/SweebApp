using SweebAppFront.Enums;
using SweebAppFront.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SweebAppFront.ViewModels
{
    public class AlertsViewModel : BaseViewModel
    {
        public ObservableCollection<Alerts> Alerts { get; set; }
        private int _counterAlerts = 0;

        public int CounterAlerts
        {
            get => _counterAlerts;
            set => SetProperty(ref _counterAlerts, value);
        }

        public AlertsViewModel()
        {
            Alerts = new ObservableCollection<Alerts>
            {
                new Alerts
                {
                    IdAlert = 1,
                    Message = "Phishing attempt blocked",
                    Severity = AlertSeverity.Critical,
                    CreatedAt = DateTime.Now.AddMinutes(-2)
                },
                new Alerts
                {
                    IdAlert = 2,
                    Message = "Suspicious login detected",
                    Severity = AlertSeverity.Warning,
                    CreatedAt = DateTime.Now.AddMinutes(-10)
                },
                new Alerts
                {

                    IdAlert = 3,
                    Message = "No threats detected",
                    Severity = AlertSeverity.Info,
                    CreatedAt = DateTime.Now.AddHours(-1)
                }
            };

            CounterAlerts = Alerts.Count();

        }

        public ICommand ChangeAlertStatus =>
         new Command<Alerts>(alert =>
         {
             alert.IsRead = true;
             CounterAlerts--;
         }
         );

        }

}
