using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _clientsService.GetAllClients();
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _clientsService.GetClientById(id);
        if (client == null)
            return NotFound();
        return Ok(client);
    }

    [HttpPost]
    public IActionResult AddClient([FromBody] Client client)
    {
        var newClient = _clientsService.AddClient(client);
        return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditClient(int id, [FromBody] Client client)
    {
        if (id != client.Id)
            return BadRequest();

        var result = await _clientsService.EditClient(client);
        if (result == Enums.OperationResult.NotFound)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var result = await _clientsService.DeleteClient(id);
        if (result == Enums.OperationResult.NotFound)
            return NotFound();
        return NoContent();
    }
}