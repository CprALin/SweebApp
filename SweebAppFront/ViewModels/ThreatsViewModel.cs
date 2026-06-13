using SweebAppAPIs.Enum;
using SweebAppFront.Enums;
using SweebAppFront.Models;
using SweebAppFront.Services;
using SweebAppFront.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;


namespace SweebAppFront.ViewModels
{
    public class ThreatsViewModel : BaseViewModel
    {
        public AlertsViewModel AlertsViewModel { get; }
        private ObservableCollection<ThreatEvent> _threatEvents = new();
        public ObservableCollection<ThreatEvent> ThreatEvents
        {
            get => _threatEvents;
            set
            {
                if (_threatEvents != value)
                {
                    _threatEvents = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<ThreatEvent> MainThreats => _threatService.ThreatEvents;

        private string _currentFilterKey = "All";
        private IThreatsService _threatService;
        private int _columns = 1;
        public int Columns
        {
            get => _columns;
            set
            {
                if (_columns != value)
                {
                    _columns = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                if(_isEmpty != value)
                {
                    _isEmpty = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand FilterCommand { get; }
        public ICommand ToggleThreatActionCommand { get; }

        public string CurrentFilterKey
        {
            get => _currentFilterKey;
            set => SetProperty(ref _currentFilterKey, value);
        }

        public ThreatsViewModel(IThreatsService threatsService, AlertsViewModel alertsViewModel)
        {
            _threatService = threatsService;
            FilterCommand = new Command<string>(SetFilter);
            ToggleThreatActionCommand = new Command<ThreatEvent>(async threat => await ToggleThreatAction(threat));
            FilterCommand.Execute(CurrentFilterKey);

            MainThreats.CollectionChanged += (_, __) =>
            {
                FilterCommand.Execute(CurrentFilterKey);
            };

            _ = LoadThreats();
            AlertsViewModel = alertsViewModel;
        }

        private async Task ToggleThreatAction(ThreatEvent? threat)
        {
            if (threat == null)
            {
                return;
            }

            var newStatus = threat.ActionTaken == ThreatStatus.Blocked
                ? ThreatStatus.Allowed
                : ThreatStatus.Blocked;

            await _threatService.UpdateAction(threat.Id, newStatus);
            FilterCommand.Execute(CurrentFilterKey);
        }

        private async Task LoadThreats()
        {
            await _threatService.LoadThreats();
            ThreatEvents.Clear();
            foreach (ThreatEvent threat in MainThreats)
            {
                ThreatEvents.Add(threat);
            }
            IsEmpty = ThreatEvents.Count == 0;
        }

        private void SetFilter(string key)
        {
            CurrentFilterKey = key;

            if (CurrentFilterKey == "All")
            {
                ThreatEvents.Clear();
                foreach (ThreatEvent threat in MainThreats)
                {
                    ThreatEvents.Add(threat);
                }
                OnPropertyChanged();
            } else if (CurrentFilterKey == "Allowed")
            {
                ThreatEvents.Clear();
                foreach (ThreatEvent threat in MainThreats.Where(t => t.ActionTaken == ThreatStatus.Allowed))
                {
                    ThreatEvents.Add(threat);
                }
                OnPropertyChanged();
            } else if (CurrentFilterKey == "Blocked")
            {
                ThreatEvents.Clear();
                foreach(ThreatEvent threat in MainThreats.Where(t => t.ActionTaken == ThreatStatus.Blocked))
                {
                    ThreatEvents.Add(threat);
                }
                OnPropertyChanged();
            }

            IsEmpty = ThreatEvents.Count == 0;
        }
    }
}
