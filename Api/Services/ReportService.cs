using Api.Data;
using Api.Helpers;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Api.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _dbcontext;

    public ReportService(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public Report GeneratePdfReport()
    {
        // Example of report generation logic.
        // Normally, you would use libraries like iTextSharp or PdfSharp to create PDFs.

        var report = new Report
        {
            Id = 1,
            ReportDate = DateTime.UtcNow,
            FilePath = "" // For simplicity, leaving it empty
        };

        // Here we would add code to generate the PDF content and store it in FileContent.
        
        _dbcontext.Reports.Add(report);
        _dbcontext.SaveChanges();

        return report;  // Return the generated report.
    }

    public async Task<ICollection<Report>> GetAllReports()
    {
        return await _dbcontext.Reports.ToListAsync();
    }

    public async Task<Report> GetReportById(int id)
    {
        return await _dbcontext.Reports.FindAsync(id);
    }

    public async Task<Enums.OperationResult> DeleteReport(List<int> ids)
    {
        var reports = await _dbcontext.Reports.Where(r => ids.Contains(r.Id)).ToListAsync();
        if (reports.Count == 0)
        {
            return Enums.OperationResult.Error;
        }

        _dbcontext.Reports.RemoveRange(reports);
        await _dbcontext.SaveChangesAsync();

        return Enums.OperationResult.Ok;
    }
}