using SweebAppFront.Enums;
using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SweebAppFront.ViewModels
{
    public class AlertsViewModel : BaseViewModel
    {
        public ObservableCollection<Alerts> Alerts => _alertService.Alerts;
 
        private int _counterAlerts = 0;

        IAlertService _alertService;
        public ICommand ClearAll { get; }
        public ICommand ClearById { get; }

        public int CounterAlerts
        {
            get => _counterAlerts;
            set => SetProperty(ref _counterAlerts, value);
        }

        public AlertsViewModel(IAlertService alertService)
        {
            _alertService = alertService;
            
            ClearAll = new Command(async() => await ClearAllAlerts());
            ClearById = new Command<int>(async (id) => await ClearAlertById(id));

            Alerts.CollectionChanged += (_, __) =>
            {
                CounterAlerts = Alerts.Count;
            };
            
            _ = LoadAlerts();
        }

        private async Task LoadAlerts()
        {
            await _alertService.GetAlerts();

            CounterAlerts = Alerts.Count();
        }

        private async Task ClearAllAlerts()
        {
            Console.WriteLine("Clearing all alerts...");
            await _alertService.ClearAllAlerts(); 
        }

        private async Task ClearAlertById(int id)
        {
            Console.WriteLine($"Clearing alert with ID: {id}...");
            await _alertService.ClearAlertById(id);
        }

    }
}
