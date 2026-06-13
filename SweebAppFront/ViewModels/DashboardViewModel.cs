using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using SweebAppAPIs.Enum;
using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SweebAppFront.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IThreatsService _threatsService;
        private readonly HttpClient _client = new();
        private readonly JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
        private readonly Uri _deviceUri = new("https://localhost:7832/api/v1/Device/get");

        private int _totalThreats;
        private int _blockedThreats;
        private int _allowedThreats;
        private string _currentDeviceName = "Unknown device";
        private string _currentDeviceOS = "Unknown OS";

        public ObservableCollection<MonthlyThreatSummary> MonthlyThreats { get; } = new();

        public ISeries[] ThreatSeries { get; private set; } = [];
        public Axis[] XAxes { get; private set; } = [];
        public Axis[] YAxes { get; private set; } = [];

        public int TotalThreats
        {
            get => _totalThreats;
            set => SetProperty(ref _totalThreats, value);
        }

        public int BlockedThreats
        {
            get => _blockedThreats;
            set => SetProperty(ref _blockedThreats, value);
        }

        public int AllowedThreats
        {
            get => _allowedThreats;
            set => SetProperty(ref _allowedThreats, value);
        }

        public string CurrentDeviceName
        {
            get => _currentDeviceName;
            set => SetProperty(ref _currentDeviceName, value);
        }

        public string CurrentDeviceOS
        {
            get => _currentDeviceOS;
            set => SetProperty(ref _currentDeviceOS, value);
        }

        public DashboardViewModel(IThreatsService threatsService)
        {
            _threatsService = threatsService;
            BuildEmptyMonths();
            ConfigureChart();

            _threatsService.ThreatEvents.CollectionChanged += (_, __) => RecalculateThreats();

            _ = LoadDashboardData();
        }

        private async Task LoadDashboardData()
        {
            await Task.WhenAll(_threatsService.LoadThreats(), LoadCurrentDevice());
            RecalculateThreats();
        }

        private async Task LoadCurrentDevice()
        {
            try
            {
                var response = await _client.GetAsync(_deviceUri);
                if (!response.IsSuccessStatusCode)
                {
                    SetLocalDeviceFallback();
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<DeviceResponse>>(json, _options);

                if (apiResponse?.Data == null)
                {
                    SetLocalDeviceFallback();
                    return;
                }

                CurrentDeviceName = string.IsNullOrWhiteSpace(apiResponse.Data.Name)
                    ? DeviceInfo.Current.Name
                    : apiResponse.Data.Name;
                CurrentDeviceOS = string.IsNullOrWhiteSpace(apiResponse.Data.OS)
                    ? DeviceInfo.Current.Platform.ToString()
                    : apiResponse.Data.OS;
            }
            catch
            {
                SetLocalDeviceFallback();
            }
        }

        private void SetLocalDeviceFallback()
        {
            CurrentDeviceName = DeviceInfo.Current.Name;
            CurrentDeviceOS = DeviceInfo.Current.Platform.ToString();
        }

        private void RecalculateThreats()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var currentYear = DateTime.Now.Year;
                var threats = _threatsService.ThreatEvents.ToList();

                TotalThreats = threats.Count;
                BlockedThreats = threats.Count(t => t.ActionTaken == ThreatStatus.Blocked);
                AllowedThreats = threats.Count(t => t.ActionTaken == ThreatStatus.Allowed);

                MonthlyThreats.Clear();

                for (var month = 1; month <= 12; month++)
                {
                    var monthThreats = threats
                        .Where(t => t.Timestamp.Year == currentYear && t.Timestamp.Month == month)
                        .ToList();

                    MonthlyThreats.Add(new MonthlyThreatSummary
                    {
                        Month = new DateTime(currentYear, month, 1).ToString("MMM"),
                        Total = monthThreats.Count,
                        Blocked = monthThreats.Count(t => t.ActionTaken == ThreatStatus.Blocked),
                        Allowed = monthThreats.Count(t => t.ActionTaken == ThreatStatus.Allowed)
                    });
                }

                ConfigureChart();
            });
        }

        private void BuildEmptyMonths()
        {
            var currentYear = DateTime.Now.Year;
            MonthlyThreats.Clear();

            for (var month = 1; month <= 12; month++)
            {
                MonthlyThreats.Add(new MonthlyThreatSummary
                {
                    Month = new DateTime(currentYear, month, 1).ToString("MMM")
                });
            }
        }

        private void ConfigureChart()
        {
            var accent = new SKColor(36, 158, 148);
            var highlight = new SKColor(59, 193, 168);
            var text = new SKColor(245, 238, 221);

            ThreatSeries =
            [
                new LineSeries<int>
                {
                    Name = "Threats",
                    Values = MonthlyThreats.Select(m => m.Total).ToArray(),
                    Stroke = new SolidColorPaint(highlight, 4),
                    Fill = new SolidColorPaint(new SKColor(59, 193, 168, 45)),
                    GeometryStroke = new SolidColorPaint(text, 2),
                    GeometryFill = new SolidColorPaint(accent),
                    GeometrySize = 10,
                    LineSmoothness = 0.45
                }
            ];

            XAxes =
            [
                new Axis
                {
                    Labels = MonthlyThreats.Select(m => m.Month).ToArray(),
                    LabelsPaint = new SolidColorPaint(text),
                    SeparatorsPaint = new SolidColorPaint(new SKColor(245, 238, 221, 35))
                }
            ];

            YAxes =
            [
                new Axis
                {
                    MinLimit = 0,
                    MinStep = 1,
                    LabelsPaint = new SolidColorPaint(text),
                    SeparatorsPaint = new SolidColorPaint(new SKColor(245, 238, 221, 35)),
                    Name = "Threats",
                    NamePaint = new SolidColorPaint(text)
                }
            ];

            OnPropertyChanged(nameof(ThreatSeries));
            OnPropertyChanged(nameof(XAxes));
            OnPropertyChanged(nameof(YAxes));
        }

        private class DeviceResponse
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string OS { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public int UserId { get; set; }
        }
    }

    public class MonthlyThreatSummary
    {
        public string Month { get; set; } = string.Empty;
        public int Total { get; set; }
        public int Blocked { get; set; }
        public int Allowed { get; set; }
    }
}
