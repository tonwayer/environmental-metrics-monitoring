using EnvironmentalMetricsService.Models;

namespace EnvironmentalMetricsService.Services
{
    public interface IAreaMetricsService
    {
        Task<Guid> RegisterAreaAsync(string areaName);
        Task RecordMetricsAsync(Guid id, MetricRecord metricRecord);
        Task<MetricRecord> GetLatestMetricsAsync(Guid id);
        Task<IEnumerable<MetricRecord>> GetMetricsHistoryAsync(Guid id, int page, int pageSize);
        Task<IEnumerable<AreaMetrics>> ListMonitoredAreasAsync();
    }
}
