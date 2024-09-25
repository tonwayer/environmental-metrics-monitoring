namespace EnvironmentalMetricsService.Models
{
    public class MetricRecord
    {
        public DateTime TimeStamp { get; set; }
        public double PowerUsage { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
