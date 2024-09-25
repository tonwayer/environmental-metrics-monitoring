﻿using EnvironmentalMetricsService.Models;

namespace EnvironmentalMetricsService.Services
{
    public class AreaMetricsService: IAreaMetricsService
    {
        private readonly Dictionary<Guid, AreaMetrics> _areas = new Dictionary<Guid, AreaMetrics>();

        public Task<Guid> RegisterAreaAsync(string areaName)
        {
            var areaId = Guid.NewGuid();
            var areaMetrics = new AreaMetrics
            {
                Id = areaId,
                AreaName = areaName,
                MetricsHistory = new List<MetricRecord>()
            };

            _areas.Add(areaId, areaMetrics);
            return Task.FromResult(areaId);
        }

        public Task RecordMetricsAsync(Guid id, MetricRecord metricRecord)
        {
            if (_areas.ContainsKey(id))
            {
                _areas[id].MetricsHistory.Add(metricRecord);
            }
            return Task.CompletedTask;
        }

        public Task<MetricRecord> GetMetricsAsync(Guid id)
        {
            if (!_areas.ContainsKey(id))
            {
                var latestMetric = _areas[id].MetricsHistory.LastOrDefault();
                return Task.FromResult(latestMetric);
            }

            return Task.FromResult<MetricRecord>(null);
        }

        public Task<IEnumerable<MetricRecord>> GetMetricsHistoryAsync(Guid id)
        {
            if (_areas.ContainsKey(id))
            {
                var history = _areas[id].MetricsHistory;
                return Task.FromResult(history.AsEnumerable());
            }

            return Task.FromResult<IEnumerable<MetricRecord>>(null);
        }

        public Task<IEnumerable<AreaMetrics>> ListMonitoredAreasAsync()
        {
            return Task.FromResult(_areas.Values.AsEnumerable());
        }
    }
}
