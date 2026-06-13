using Microsoft.Win32;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
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
        private Uri uriThreatByUrl = new Uri("https://localhost:7832/api/v1/Threats/by-url");
        private Uri uriAlert = new Uri("https://localhost:7832/api/v1/Alerts/create");
        private readonly ProxyServer _proxyServer;
        private readonly ISignalRService _signalRService;
        private static DateTime _lastSent = DateTime.MinValue;
        private static readonly HashSet<string> DocumentFetchDestinations = new(StringComparer.OrdinalIgnoreCase)
        {
            "document",
            "iframe",
            "frame",
            "nested-document"
        };

        private static readonly HashSet<string> IgnoredExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".js", ".css", ".png", ".jpg", ".jpeg", ".gif", ".svg", ".ico", ".webp", ".avif",
            ".bmp", ".map", ".json", ".xml", ".txt", ".woff", ".woff2", ".ttf", ".otf", ".eot",
            ".mp4", ".webm", ".mp3", ".m4a", ".wasm", ".zip", ".gz", ".br"
        };

        private static readonly string[] IgnoredHostParts =
        [
            "msftconnecttest.com",
            "msftncsi.com",
            "windowsupdate.com",
            "update.microsoft.com",
            "delivery.mp.microsoft.com",
            "settings-win.data.microsoft.com",
            "v10.events.data.microsoft.com",
            "safebrowsing.googleapis.com",
            "update.googleapis.com",
            "optimizationguide-pa.googleapis.com",
            "clients4.google.com",
            "clients2.google.com",
            "detectportal.firefox.com",
            "push.services.mozilla.com",
            "ocsp.",
            "crl."
        ];

        private static readonly string[] IgnoredPathParts =
        [
            "/favicon",
            "/generate_204",
            "/gen_204",
            "/connecttest.txt",
            "/ncsi.txt",
            "/success.txt",
            "/canonical.html",
            "/hotword",
            "/telemetry",
            "/analytics",
            "/collect",
            "/beacon",
            "/metrics",
            "/events",
            "/ping",
            "/socket",
            "/websocket",
            "/poll",
            "/assets/",
            "/static/"
        ];

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

        private async Task<ApiResponse<ThreatEvent>> GetLatestThreatByUrl(string url)
        {
            try
            {
                var requestUri = new Uri($"{uriThreatByUrl}?url={Uri.EscapeDataString(url)}");
                HttpResponseMessage response = await _client.GetAsync(requestUri);
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<ThreatEvent>>(responseContent, _options);

                if (apiResponse == null)
                {
                    return new ApiResponse<ThreatEvent>
                    {
                        Status = "Error",
                        Message = "API response is null.",
                        Data = null
                    };
                }

                return apiResponse;
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
                Console.WriteLine(json);
                HttpResponseMessage response = await _client.PostAsync(uriThreat, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine("========== RESPONSE DEBUG ==========");
                Console.WriteLine("StatusCode: " + response.StatusCode);
                Console.WriteLine("Headers: " + response.Headers);
                Console.WriteLine("Content:");
                Console.WriteLine(responseContent);
                Console.WriteLine("====================================");

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
            if (!Uri.TryCreate(e.HttpClient.Request.Url, UriKind.Absolute, out var requestUri))
                return;

            if (!ShouldInspectRequest(e, requestUri))
                return;

            var url = NormalizePageUrl(requestUri);
            var host = requestUri.Host.ToLowerInvariant();
            var protocol = requestUri.Scheme.ToLowerInvariant();
            var method = e.HttpClient.Request.Method.ToUpper();
            var port = requestUri.Port;

            var knownThreat = await GetLatestThreatByUrl(url);
            var isAllowedThreat = knownThreat.Data?.ActionTaken == ThreatStatus.Allowed;
            var isBlockedThreat = knownThreat.Data?.ActionTaken == ThreatStatus.Blocked;
            var checkUrl = isAllowedThreat
                ? new ApiResponse<ModelResponse>
                {
                    Status = "Success",
                    Message = "Threat was allowed manually.",
                    Data = new ModelResponse
                    {
                        Prediction = new Prediction
                        {
                            Label = "threat",
                            Score = knownThreat.Data?.Score ?? 0
                        }
                    }
                }
                : await CheckUrl(url);

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
                if (isAllowedThreat)
                {
                    Console.WriteLine($"[Allowed Threat] {url} was manually allowed. Request continues.");
                }
                else
                {
                    Console.WriteLine("Creating threat case ...");
                    var threatCase = isBlockedThreat && knownThreat.Data != null
                        ? knownThreat
                        : await IsThreatCase(url, protocol, checkUrl?.Data?.Prediction?.Score ?? 0);
                    Console.WriteLine($"Threat case response : {threatCase.Message}");

                    await SendLiveTraffic(response);
                    await BlockThreatRequest(e, url);
                    Console.WriteLine($"[Blocked] {url}");
                    return;
                }
            }

            await SendLiveTraffic(response);
            Console.WriteLine($"[Request] {response.Method} | {response.Protocol} | {response.Url} | {response.Host} | {response.Port} | {response.IsThreat}");
        }

        private static bool ShouldInspectRequest(SessionEventArgs e, Uri requestUri)
        {
            var method = e.HttpClient.Request.Method;
            if (!string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (requestUri.Scheme != Uri.UriSchemeHttp && requestUri.Scheme != Uri.UriSchemeHttps)
                return false;

            var host = requestUri.Host.ToLowerInvariant();
            if (IsLocalOrPrivateHost(host))
                return false;

            if (IgnoredHostParts.Any(host.Contains))
                return false;

            var path = requestUri.AbsolutePath.ToLowerInvariant();
            var extension = Path.GetExtension(path);
            if (!string.IsNullOrWhiteSpace(extension) && IgnoredExtensions.Contains(extension))
                return false;

            if (IgnoredPathParts.Any(path.Contains))
                return false;

            var secFetchDest = GetHeaderValue(e, "Sec-Fetch-Dest");
            if (!string.IsNullOrWhiteSpace(secFetchDest) && !DocumentFetchDestinations.Contains(secFetchDest))
                return false;

            var secFetchMode = GetHeaderValue(e, "Sec-Fetch-Mode");
            if (!string.IsNullOrWhiteSpace(secFetchMode) &&
                !string.Equals(secFetchMode, "navigate", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var accept = GetHeaderValue(e, "Accept");
            if (!string.IsNullOrWhiteSpace(accept) &&
                !accept.Contains("text/html", StringComparison.OrdinalIgnoreCase) &&
                !accept.Contains("application/xhtml+xml", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var userAgent = GetHeaderValue(e, "User-Agent");
            if (!string.IsNullOrWhiteSpace(userAgent) &&
                !userAgent.Contains("Mozilla", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private static string NormalizePageUrl(Uri requestUri)
        {
            var builder = new UriBuilder(requestUri)
            {
                Fragment = string.Empty
            };

            return builder.Uri.ToString();
        }

        private static string GetHeaderValue(SessionEventArgs e, string headerName)
        {
            var headerText = e.HttpClient.Request.HeaderText;
            if (string.IsNullOrWhiteSpace(headerText))
                return string.Empty;

            var lines = headerText.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var separatorIndex = line.IndexOf(':');
                if (separatorIndex <= 0)
                    continue;

                var name = line[..separatorIndex].Trim();
                if (string.Equals(name, headerName, StringComparison.OrdinalIgnoreCase))
                    return line[(separatorIndex + 1)..].Trim();
            }

            return string.Empty;
        }

        private static bool IsLocalOrPrivateHost(string host)
        {
            if (host is "localhost" or "0.0.0.0")
                return true;

            if (!IPAddress.TryParse(host, out var ipAddress))
                return false;

            if (IPAddress.IsLoopback(ipAddress))
                return true;

            if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                var bytes = ipAddress.GetAddressBytes();
                return bytes[0] == 10 ||
                       bytes[0] == 127 ||
                       bytes[0] == 169 && bytes[1] == 254 ||
                       bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31 ||
                       bytes[0] == 192 && bytes[1] == 168;
            }

            return ipAddress.AddressFamily == AddressFamily.InterNetworkV6 &&
                   (ipAddress.IsIPv6LinkLocal || ipAddress.IsIPv6SiteLocal);
        }

        private async Task SendLiveTraffic(ResponseProxy response)
        {
            if ((DateTime.Now - _lastSent).TotalSeconds < 3)
                return;

            await _signalRService.SendProxyTrafficAsync(response);
            _lastSent = DateTime.Now;
        }

        private async Task BlockThreatRequest(SessionEventArgs e, string url)
        {
            var body = $$"""
                <!doctype html>
                <html>
                <head>
                    <meta charset="utf-8">
                    <title>SweebApp blocked this request</title>
                    <style>
                        body { font-family: Segoe UI, Arial, sans-serif; background: #0f172a; color: #f8fafc; margin: 0; display: grid; place-items: center; min-height: 100vh; }
                        main { max-width: 720px; padding: 32px; border: 1px solid #ef4444; border-radius: 12px; background: #111827; }
                        h1 { color: #fca5a5; margin-top: 0; }
                        code { color: #fde68a; word-break: break-all; }
                    </style>
                </head>
                <body>
                    <main>
                        <h1>Request blocked</h1>
                        <p>SweebApp blocked this request because it was detected as a potential threat.</p>
                        <p><code>{{WebUtility.HtmlEncode(url)}}</code></p>
                        <p>You can allow it from the Threats screen, but you are responsible for the potential risk.</p>
                    </main>
                </body>
                </html>
                """;

            e.Ok(body);
            await Task.CompletedTask;
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
