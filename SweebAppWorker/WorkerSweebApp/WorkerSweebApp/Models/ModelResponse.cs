using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerSweebApp.Models
{
    public class ModelResponse
    {
        public string Url { get; set; } = string.Empty;
        public Prediction? Prediction { get; set; }
    }

    public class Prediction
{
    public string Label { get; set; } = string.Empty;
    public double Score { get; set; }
}
}
