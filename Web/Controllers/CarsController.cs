using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Web.Controllers;

public class CarsController : Controller
{
    private readonly HttpClient _httpClient;

    public CarsController(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("ApiClient");
    }

    public async Task<IActionResult> Index()
    {
        var cars = await _httpClient.GetFromJsonAsync<List<Car>>("api/cars");
        return View(cars);
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var car = await _httpClient.GetFromJsonAsync<List<Car>>($"api/cars?id={id}");
        if (car == null)
            return NotFound();

        return View(car.First());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Car car)
    {
        var response = await _httpClient.PostAsJsonAsync("api/cars", car);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Failed to create the car");
        return View(car);
    }
    [HttpPut]
    public async Task<IActionResult> Edit(int id)
    {
        var car = await _httpClient.GetFromJsonAsync<Car>($"api/cars/GetCarById/{id}");
        if (car == null)
            return NotFound();

        return View(car);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Car car)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/cars/{car.Id}", car);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Failed to edit the car");
        return View(car);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/cars/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return BadRequest("Failed to delete the car");
    }
}
