using EnvironmentalMetricsService.Models;

namespace EnvironmentalMetricsService.Services
{
    public interface IAreaMetricsService
    {
        Task<Guid> RegisterAreaAsync(string areaName);
        Task RecordMetricsAsync(Guid id, MetricRecord metricRecord);
        Task<MetricRecord> GetMetricsAsync(Guid id);
        Task<IEnumerable<MetricRecord>> GetMetricsHistoryAsync(Guid id);
        Task<IEnumerable<AreaMetrics>> ListMonitoredAreasAsync();
    }
}
