using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using WorkerSweebApp.Services.Interfaces;

namespace WorkerSweebApp.Services
{
    public class ProxyService : IProxyService
    {
        private readonly ProxyServer _proxyServer;

        public ProxyService()
        {
            _proxyServer = new ProxyServer();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _proxyServer.CertificateManager.EnsureRootCertificate();

            var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true);

            _proxyServer.AddEndPoint(explicitEndPoint);

            _proxyServer.BeforeRequest += OnRequest;
 
            _proxyServer.Start();

            Console.WriteLine("Proxy server started on port 8000.");
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

            string[] ignoreExt = { ".js",".css",".png",".jpg",".jpeg",".gif",".svg",".ico",".woff",".woff2",".ttf",".map",".json"};

            if (ignoreExt.Any(ext => url.Contains(ext)))
                return;

            //Console.WriteLine($"[REQUEST] {method} {url}");
            Console.WriteLine($"[Request] {method} {protocol} {path} {url} {host}");
        }
    }
}
