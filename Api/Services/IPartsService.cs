using Api.Helpers;
using Api.Models;

namespace Api.Services;

public interface IPartsService
{
    public Task<Part> AddPart(Part part);

    public Task<Enums.OperationResult> EditPart(Part part);

    public Task<Enums.OperationResult> DeletePart(int id);
    
    public Task<ICollection<Part>> GetAllParts();
    
    public Task<Part> GetPartById(int id);
}