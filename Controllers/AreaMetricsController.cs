using EnvironmentalMetricsService.Models;
using EnvironmentalMetricsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentalMetricsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaMetricsController: ControllerBase
    {
        private readonly IAreaMetricsService _areaMetricsService;

        public AreaMetricsController(IAreaMetricsService areaMetricsService)
        {
            _areaMetricsService = areaMetricsService;
        }

        [HttpPost("areas")]
        public async Task<IActionResult> RegisterArea([FromBody] string areaName)
        {
            var areaId = await _areaMetricsService.RegisterAreaAsync(areaName);
            return Ok(areaId);
        }

        [HttpPost("areas/{id}/metrics")]
        public async Task<IActionResult> RecordMetrics(Guid id, [FromBody] MetricRecord metricRecord)
        {
            await _areaMetricsService.RecordMetricsAsync(id, metricRecord);
            return Ok();
        }

        [HttpGet("areas/{id}/metrics/latest")]
        public async Task<IActionResult> GetLatestMetrics(Guid id)
        {
            var latestMetrics = await _areaMetricsService.GetLatestMetricsAsync(id);
            return Ok(latestMetrics);
        }

        [HttpGet("areas/{id}/metrics/history")]
        public async Task<IActionResult> GetMetricsHistory(Guid id, int page = 1, int pageSize = 10)
        {
            var metricsHistory = await _areaMetricsService.GetMetricsHistoryAsync(id, page, pageSize);
            return Ok(metricsHistory);
        }

        [HttpGet("areas")]
        public async Task<IActionResult> ListMonitoredAreas()
        {
            var areas = await _areaMetricsService.ListMonitoredAreasAsync();
            return Ok(areas);
        }
    }
}
