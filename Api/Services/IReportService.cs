using Api.Helpers;
using Api.Models;

namespace Api.Services;

public interface IReportService
{
    
    public Report GeneratePdfReport();
    
    public Task<ICollection<Report>> GetAllReports();
    
    public Task<Report> GetReportById(int id);

    public Task<Enums.OperationResult> DeleteReport(List<int> ids);
}