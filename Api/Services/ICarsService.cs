using Api.Helpers;
using Api.Models;

namespace Api.Services;

public interface ICarsService
{
    public Task<Car> AddCar(Car car);

    public Task<Enums.OperationResult> EditCar(Car car);

    public Task<Enums.OperationResult> DeleteCar(int id);
    
    public Task<ICollection<Car>> GetAllCars();
    
    public Task<Car> GetCarById(int id);
}