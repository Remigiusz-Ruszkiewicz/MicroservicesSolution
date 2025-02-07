using Api.Data;
using Api.Helpers;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class CarsService : ICarsService
{
    private readonly AppDbContext _dbcontext;

    public CarsService(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Car> AddCar(Car car)
    {
        await _dbcontext.Cars.AddAsync(car);
        await _dbcontext.SaveChangesAsync();
        return car;
    }

    public async Task<Enums.OperationResult> EditCar(Car car)
    {
        var existingCar = await _dbcontext.Cars.FindAsync(car.Id);
        if (existingCar == null)
        {
            return Enums.OperationResult.Error;  // Return failure if car not found
        }

        existingCar.Make = car.Make;
        existingCar.Model = car.Model;
        existingCar.Year = car.Year;
        existingCar.Status = car.Status;

        _dbcontext.Cars.Update(existingCar);
        await _dbcontext.SaveChangesAsync();

        return Enums.OperationResult.Ok; // Return success after update
    }

    public async Task<Enums.OperationResult> DeleteCar(int id)
    {
        var car = await _dbcontext.Cars.FindAsync(id);
        if (car == null)
        {
            return Enums.OperationResult.Error;
        }

        _dbcontext.Cars.Remove(car);
        await _dbcontext.SaveChangesAsync();

        return Enums.OperationResult.Ok;
    }

    public async Task<ICollection<Car>> GetAllCars()
    {
        return await _dbcontext.Cars.ToListAsync();
    }

    public async Task<Car> GetCarById(int id)
    {
        return await _dbcontext.Cars.FindAsync(id);
    }
}