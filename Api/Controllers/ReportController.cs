using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _reportService.GetAllReports();
        return Ok(reports);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetReportById(int id)
    {
        var report = await _reportService.GetReportById(id);
        if (report == null)
            return NotFound();
        return Ok(report);
    }

    [HttpPost("generate")]
    public IActionResult GeneratePdfReport()
    {
        var report = _reportService.GeneratePdfReport();
        return Ok(report);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReports([FromBody] List<int> ids)
    {
        var result = await _reportService.DeleteReport(ids);
        if (result == Enums.OperationResult.NotFound)
            return NotFound();
        return NoContent();
    }
}