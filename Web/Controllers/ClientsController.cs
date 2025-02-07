using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Web.Controllers;

public class ClientsController : Controller
{
    private readonly HttpClient _httpClient;

    public ClientsController(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("ApiClient");
    }

    public async Task<IActionResult> Index()
    {
        var clients = await _httpClient.GetFromJsonAsync<List<Client>>("api/clients");
        return View(clients);
    }

    public async Task<IActionResult> Details(int id)
    {
        var client = await _httpClient.GetFromJsonAsync<Client>($"api/clients/{id}");
        if (client == null)
            return NotFound();

        return View(client);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Client client)
    {
        var response = await _httpClient.PostAsJsonAsync("api/clients", client);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Failed to create the client");
        return View(client);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = await _httpClient.GetFromJsonAsync<Client>($"api/clients/{id}");
        if (client == null)
            return NotFound();

        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Client client)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/clients/{client.Id}", client);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Failed to edit the client");
        return View(client);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/clients/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return BadRequest("Failed to delete the client");
    }
}
