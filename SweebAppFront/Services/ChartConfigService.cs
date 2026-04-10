using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using SweebAppFront.Services.Interfaces;

/* 
new SKColor(0, 84, 97)      // Primary   #005461
new SKColor(12, 119, 121)   // Variant   #0C7779
new SKColor(36, 158, 148)   // Accent    #249E94
new SKColor(59, 193, 168)   // Highlight #3BC1A8
new SKColor(245, 238, 221)  // TextPrimary   #F5EEDD
 */

namespace SweebAppFront.Services
{
    public class ChartConfigService : IChartConfigService
    {
        private readonly SKColor primary = new(0, 84, 97);
        private readonly SKColor variant = new(12, 119, 121);
        private readonly SKColor accent = new(36, 158, 148);

        public Axis[] CreateMounthsXAxis(string name)
        {
            return [
                new Axis {
                    Name = name,
                    NamePaint = new SolidColorPaint(primary),
                    LabelsPaint = new SolidColorPaint(primary)
                }
            ];
        }

        public Axis[] CreateThreatsYAxis(string name)
        {
            return[
            
            ];
        }
    }
}
