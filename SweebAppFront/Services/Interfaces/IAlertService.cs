using SweebAppFront.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SweebAppFront.Services.Interfaces
{
    public interface IAlertService
    {
        ObservableCollection<Alerts> Alerts { get; set; }
        Task GetAlerts();
        Task ClearAllAlerts();
        Task ClearAlertById(int id);
    }
}
