
using LiveChartsCore.SkiaSharpView;

namespace SweebAppFront.Services.Interfaces
{
    public interface IChartConfigService
    {
        Axis[] CreateMounthsXAxis(string name);
        Axis[] CreateThreatsYAxis(string name);
    }
}
