using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using WorkerSweebApp.Models;
using WorkerSweebApp.Services.Interfaces;

namespace WorkerSweebApp.Services
{
    public class ProxyService : IProxyService
    {
        private readonly ProxyServer _proxyServer;
        private readonly ISignalRService _signalRService;

        public ProxyService(ISignalRService signalRService)
        {
            _proxyServer = new ProxyServer();
            _signalRService = signalRService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            SetSystemProxy("127.0.0.1:8000");
            _proxyServer.CertificateManager.EnsureRootCertificate();

            var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true);

            _proxyServer.AddEndPoint(explicitEndPoint);

            _proxyServer.BeforeRequest += OnRequest;

            _proxyServer.Start();

            Console.WriteLine("Proxy server started on port 8000.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            RemoveSystemProxy();

            _proxyServer.Stop();

            Console.WriteLine("Proxy server stopped.");
        }

        private async Task OnRequest(Object sender, SessionEventArgs e)
        {
            if (e.HttpClient.Request.Method != "GET")
                return;

            var uri = new Uri(e.HttpClient.Request.Url.ToLower());
            var url = uri.GetLeftPart(UriPartial.Authority);
            var path = uri.AbsolutePath;
            var host = uri.Host;
            var protocol = uri.Scheme;
            var method = e.HttpClient.Request.Method.ToUpper();
            var port = uri.Port;

            if (path.Contains("/static") || path.Contains("/assets"))
                return;

            var response = new ResponseProxy
            {
                Method = method,
                Protocol = protocol,
                Url = url,
                Host = host,
                Port = port,
                IsThreat = false
            };

            await _signalRService.SendProxyTrafficAsync(response);
            Console.WriteLine($"[Request] {response.Method} | {response.Protocol} | {response.Url} | {response.Host} | {response.Port} | {response.IsThreat}");
        }

        private void SetSystemProxy(string proxyAddress) 
        {
            if (OperatingSystem.IsWindows())
            {
                var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

                key?.SetValue("ProxyEnable", 1);
                key?.SetValue("ProxyServer", proxyAddress);
            }
        }

        public void RemoveSystemProxy()
        {
            if (OperatingSystem.IsWindows())
            {
                var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

                key?.SetValue("ProxyEnable", 0);
            }
        }
    }
}
