using Microsoft.Win32;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using WorkerSweebApp.Models;
using WorkerSweebApp.Services.Interfaces;

namespace WorkerSweebApp.Services
{
    public class ProxyService : IProxyService
    {
        private HttpClient _client = new HttpClient();
        private JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
        private Uri uri = new Uri("http://127.0.0.1:7833/model_predict");
        private Uri uriDevice = new Uri("https://localhost:7832/api/v1/Device/get");
        private Uri uriThreat = new Uri("https://localhost:7832/api/v1/Threats/create");
        private Uri uriAlert = new Uri("https://localhost:7832/api/v1/Alerts/create");
        private readonly ProxyServer _proxyServer;
        private readonly ISignalRService _signalRService;
        private static DateTime _lastSent = DateTime.MinValue;

        public ProxyService(ISignalRService signalRService)
        {
            _proxyServer = new ProxyServer();
            _signalRService = signalRService;
        }

        private async Task<ApiResponse<ModelResponse>> CheckUrl(string url)
        {
            try
            {
                string json = JsonSerializer.Serialize(new {url = url}, _options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ModelResponse>(responseContent, _options);
                
                if(apiResponse == null)
                {
                    return new ApiResponse<ModelResponse>
                    {
                        Status = "Error",
                        Message = "API response is null.",
                        Data = null
                    };
                }
                return new ApiResponse<ModelResponse>
                {
                    Status = "Success",
                    Message = "Model response received.",
                    Data = apiResponse
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ModelResponse> { 
                     Status = "Error",
                     Message = ex.Message,
                     Data = null
                };
            }
        }

        public async Task<ApiResponse<Alert>> SendAlert(int threatId, string message)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { 
                    threatId = threatId,
                    message = message,
                    severity = 1
                }, _options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uriAlert, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<Alert>>(responseContent, _options);
                if (apiResponse == null || apiResponse.Data == null)
                {
                    return new ApiResponse<Alert>
                    {
                        Status = "Error",
                        Message = "API response is null.",
                        Data = null
                    };
                }

                Console.WriteLine("Creating alert ...");
                return new ApiResponse<Alert>
                {
                    Status = "Success",
                    Message = "Alert created.",
                    Data = apiResponse.Data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Alert>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        private async Task<ApiResponse<Device>> GetDeviceId()
        {
            try 
            {
                HttpResponseMessage response = await _client.GetAsync(uriDevice);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<Device>>(responseContent, _options);

                if (apiResponse == null || apiResponse.Data == null)
                {
                    return new ApiResponse<Device>
                    {
                        Status = "Error",
                        Message = "API response is null.",
                        Data = null
                    };
                }

                Console.WriteLine($"Data : {apiResponse.Data.Id}");
                return new ApiResponse<Device>
                {
                    Status = "Success",
                    Message = "Device information received.",
                    Data = apiResponse.Data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Device>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        private async Task<ApiResponse<ThreatEvent>> IsThreatCase(string url, string protocol, double score)
        {
            try
            {
                Console.WriteLine("Getting device info...");
                var responseDevice = await GetDeviceId();
                if(responseDevice == null || responseDevice.Data == null)
                {
                    return new ApiResponse<ThreatEvent>
                    {
                        Status = "Error",
                        Message = "Failed to retrieve device information.",
                        Data = null
                    };
                }
                Console.WriteLine($"Successfull device info... : Message : {responseDevice.Message} | Id : {responseDevice.Data.Id}");
                string json = JsonSerializer.Serialize(new
                {
                    deviceId = responseDevice.Data?.Id,
                    url = url,
                    protocol = protocol,
                    verdict = "Phishing",
                    status = ThreatStatus.Blocked,
                    score = score,
                    category = "Credential Harvesting"
                }, _options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine("Begin call api threat...");
                HttpResponseMessage response = await _client.PostAsync(uriThreat, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<ThreatEvent>>(responseContent, _options);

                if (apiResponse == null || apiResponse.Data == null)
                {
                    return new ApiResponse<ThreatEvent>
                    {
                        Status = "Error",
                        Message = "API response is null.",
                        Data = null
                    };
                }

                Console.WriteLine($"Sending alert for threat : {apiResponse.Data.Id}");
                await SendAlert(apiResponse.Data.Id, "Potential phishing attempt detected.");
                return new ApiResponse<ThreatEvent>
                {
                    Status = "Success",
                    Message = "Threat event created.",
                    Data = apiResponse.Data
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<ThreatEvent>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
            }
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


            if (path.EndsWith(".js") ||
                path.EndsWith(".css") ||
                path.EndsWith(".png") ||
                path.EndsWith(".jpg") ||
                path.EndsWith(".svg") ||
                path.Contains("/static") ||
                path.Contains("socket") ||
                path.Contains("websocket") ||
                path.Contains("poll") ||
                path.Contains("update") ||
                path.Contains("/assets"))
                return;

            if (url.Contains("localhost") || url.Contains("127.0.0.1") || url.Contains("0.0.0.0"))
            {
                return;
            }

            var checkUrl = await CheckUrl(url);
            Console.WriteLine($"[Check URL] {url} | Label : {checkUrl.Data?.Prediction?.Label} | Score : {checkUrl.Data?.Prediction?.Score}");
            var response = new ResponseProxy
            {
                Method = method,
                Protocol = protocol,
                Url = url,
                Host = host,
                Port = port,
                IsThreat = checkUrl?.Data?.Prediction?.Label == "threat"
            };

            if (response.IsThreat)
            {
                Console.WriteLine("Creating threat case ...");
                var threatCase = await IsThreatCase(url, protocol, checkUrl?.Data?.Prediction?.Score ?? 0);
                Console.WriteLine($"Threat case response : {threatCase.Message}");
            }

            if ((DateTime.Now - _lastSent).TotalSeconds< 3)
                return;

            await _signalRService.SendProxyTrafficAsync(response);
            _lastSent = DateTime.Now;
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
