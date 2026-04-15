using System;
using System.Collections.Generic;
using System.Text;
using WorkerSweebApp.Models;

namespace WorkerSweebApp.Services.Interfaces
{
    public interface ISignalRService
    {
        Task StartAsync();
        Task StopAsync();
        Task SendProxyTrafficAsync(ResponseProxy response);
    }
}
