using Api.Data;
using Api.Helpers;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PartsService : IPartsService
{
    private readonly AppDbContext _dbcontext;

    public PartsService(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Part> AddPart(Part part)
    {
        await _dbcontext.Parts.AddAsync(part);
        await _dbcontext.SaveChangesAsync();
        return part;
    }

    public async Task<Enums.OperationResult> EditPart(Part part)
    {
        var existingPart = await _dbcontext.Parts.FindAsync(part.Id);
        if (existingPart == null)
        {
            return Enums.OperationResult.Error;  // Part not found
        }

        existingPart.PartName = part.PartName;
        existingPart.Price = part.Price;
        existingPart.PartCondition = part.PartCondition;

        _dbcontext.Parts.Update(existingPart);
        await _dbcontext.SaveChangesAsync();

        return Enums.OperationResult.Ok; // Update success
    }

    public async Task<Enums.OperationResult> DeletePart(int id)
    {
        var part = await _dbcontext.Parts.FindAsync(id);
        if (part == null)
        {
            return Enums.OperationResult.Error;  // Part not found
        }

        _dbcontext.Parts.Remove(part);
        await _dbcontext.SaveChangesAsync();

        return Enums.OperationResult.Ok;
    }

    public async Task<ICollection<Part>> GetAllParts()
    {
        return await _dbcontext.Parts.ToListAsync();
    }

    public async Task<Part> GetPartById(int id)
    {
        return await _dbcontext.Parts.FindAsync(id);
    }
}