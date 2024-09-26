using System.ComponentModel.DataAnnotations;

namespace EnvironmentalMetricsService.Models
{
    public class AreaMetrics
    {
        public Guid Id { get; set; }
        [Required]
        public string AreaName { get; set; } = string.Empty;
        public List<MetricRecord> MetricsHistory { get; set; } = new List<MetricRecord>();
    }
}
