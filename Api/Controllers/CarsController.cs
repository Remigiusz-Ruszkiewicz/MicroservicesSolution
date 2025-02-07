using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly ICarsService _carsService;

    public CarsController(ICarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCars()
    {
        var cars = await _carsService.GetAllCars();
        return Ok(cars);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCarById(int id)
    {
        var car = await _carsService.GetCarById(id);
        if (car == null)
            return NotFound();
        return Ok(car);
    }

    [HttpPost]
    public IActionResult AddCar([FromBody] Car car)
    {
        var newCar = _carsService.AddCar(car);
        return CreatedAtAction(nameof(GetCarById), new { id = newCar.Id }, newCar);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditCar(int id, [FromBody] Car car)
    {
        if (id != car.Id)
            return BadRequest();

        var result = await _carsService.EditCar(car);
        if (result == Enums.OperationResult.NotFound)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var result = await _carsService.DeleteCar(id);
        if (result == Enums.OperationResult.NotFound)
            return NotFound();
        return NoContent();
    }
}