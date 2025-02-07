using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Web.Controllers;

public class PartsController : Controller
{
    private readonly HttpClient _httpClient;

    public PartsController(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("ApiClient");
    }

    public async Task<IActionResult> Index()
    {
        var parts = await _httpClient.GetFromJsonAsync<List<Part>>("api/parts");
        return View(parts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var part = await _httpClient.GetFromJsonAsync<Part>($"api/parts/{id}");
        if (part == null)
            return NotFound();

        return View(part);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Part part)
    {
        var response = await _httpClient.PostAsJsonAsync("api/parts", part);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Failed to create the part");
        return View(part);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var part = await _httpClient.GetFromJsonAsync<Part>($"api/parts/{id}");
        if (part == null)
            return NotFound();

        return View(part);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Part part)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/parts/{part.Id}", part);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Failed to edit the part");
        return View(part);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/parts/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return BadRequest("Failed to delete the part");
    }
}
