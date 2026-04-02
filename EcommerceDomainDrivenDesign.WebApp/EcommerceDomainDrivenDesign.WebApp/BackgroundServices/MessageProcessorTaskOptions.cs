using System;

namespace EcommerceDomainDrivenDesign.WebApp.BackgroundServices
{
    public class MessageProcessorTaskOptions
    {
        public double IntervalOnSeconds { get; set; }
        public int BatchSize { get; set; }
    }
}
