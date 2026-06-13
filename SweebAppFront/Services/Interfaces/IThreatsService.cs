using SweebAppAPIs.Enum;
using SweebAppAPIs.Models;
using SweebAppFront.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SweebAppFront.Services.Interfaces
{
    public interface IThreatsService
    {
        ObservableCollection<ThreatEvent> ThreatEvents { get; set; }
        Task LoadThreats();
        Task UpdateAction(int threatId, ThreatStatus status);
    }
}
