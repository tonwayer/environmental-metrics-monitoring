using System;
using System.Collections.Generic;

namespace EnvironmentalMetricsService.Models
{
    public class AreaMetrics
    {
        public Guid Id { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public List<MetricRecord> MetricsHistory { get; set; } = new List<MetricRecord>();
    }
}
