using WorkerSweebApp;
using WorkerSweebApp.Services;
using WorkerSweebApp.Services.Interfaces;
using WorkerSweebApp.Workers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IProxyService, ProxyService>();
builder.Services.AddHostedService<ProxyWorker>();

var host = builder.Build();
host.Run();
