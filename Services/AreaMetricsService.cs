﻿using EnvironmentalMetricsService.Models;

namespace EnvironmentalMetricsService.Services
{
    public class AreaMetricsService : IAreaMetricsService
    {
        private readonly Dictionary<Guid, AreaMetrics> _areas = [];

        public Task<Guid> RegisterAreaAsync(string areaName)
        {
            if (string.IsNullOrWhiteSpace(areaName))
            {
                throw new ArgumentException("Area name cannot be empty.");
            }

            if (_areas.Values.Any(a => a.AreaName == areaName))
            {
                throw new ArgumentException("Area name cannot be duplicated.");
            }

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
            if (!_areas.ContainsKey(id))
            {
                throw new KeyNotFoundException($"Area with ID {id} not found.");
            }
            _areas[id].MetricsHistory.Add(metricRecord);
            return Task.CompletedTask;
        }

        public Task<MetricRecord> GetLatestMetricsAsync(Guid id)
        {
            if (_areas.ContainsKey(id))
            {
                var latestMetric = _areas[id].MetricsHistory.LastOrDefault();
                if (latestMetric != null)
                {
                    return Task.FromResult(latestMetric);
                }

                throw new InvalidOperationException($"No metrics found for area with ID {id}.");
            }

            throw new KeyNotFoundException($"Area with ID {id} not found.");
        }

        public Task<IEnumerable<MetricRecord>> GetMetricsHistoryAsync(Guid id, int page = 1, int pageSize = 10)
        {
            if (!_areas.ContainsKey(id)) return Task.FromResult<IEnumerable<MetricRecord>>([]);

            var metricsHistory = _areas[id].MetricsHistory
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            return Task.FromResult(metricsHistory);
        }

        public Task<IEnumerable<MetricRecord>> GetMetricsHistoryByTimeRangeAsync(Guid id, DateTime startTime, DateTime endTime)
        {
            if (!_areas.ContainsKey(id)) return Task.FromResult<IEnumerable<MetricRecord>>([]);

            var filteredMetrics = _areas[id].MetricsHistory
                                .Where(m => m.TimeStamp >= startTime && m.TimeStamp <= endTime);
            return Task.FromResult(filteredMetrics);
        }

        public Task<IEnumerable<AreaMetrics>> ListMonitoredAreasAsync()
        {
            return Task.FromResult(_areas.Values.AsEnumerable());
        }
    }
}
