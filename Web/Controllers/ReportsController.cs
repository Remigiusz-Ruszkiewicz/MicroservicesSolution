using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Web.Controllers;

public class ReportsController : Controller
{
    private readonly HttpClient _httpClient;

    public ReportsController(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("ApiClient");
    }

    public async Task<IActionResult> Index()
    {
        var reports = await _httpClient.GetFromJsonAsync<List<Report>>("api/reports");
        return View(reports);
    }

    public async Task<IActionResult> Details(int id)
    {
        var report = await _httpClient.GetFromJsonAsync<Report>($"api/reports/{id}");
        if (report == null)
            return NotFound();

        return View(report);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Report report)
    {
        var response = await _httpClient.PostAsJsonAsync("api/reports/generate", report);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Failed to create the report");
        return View(report);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var report = await _httpClient.GetFromJsonAsync<Report>($"api/reports/{id}");
        if (report == null)
            return NotFound();

        return View(report);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Report report)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/reports/{report.Id}", report);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Failed to edit the report");
        return View(report);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/reports/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return BadRequest("Failed to delete the report");
    }
}
