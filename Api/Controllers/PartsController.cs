using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartsController : ControllerBase
{
    private readonly IPartsService _partsService;

    public PartsController(IPartsService partsService)
    {
        _partsService = partsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllParts()
    {
        var parts = await _partsService.GetAllParts();
        return Ok(parts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPartById(int id)
    {
        var part = await _partsService.GetPartById(id);
        if (part == null)
            return NotFound();
        return Ok(part);
    }

    [HttpPost]
    public IActionResult AddPart([FromBody] Part part)
    {
        var newPart = _partsService.AddPart(part);
        return CreatedAtAction(nameof(GetPartById), new { id = newPart.Id }, newPart);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditPart(int id, [FromBody] Part part)
    {
        if (id != part.Id)
            return BadRequest();

        var result = await _partsService.EditPart(part);
        if (result == Enums.OperationResult.NotFound)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePart(int id)
    {
        var result =await _partsService.DeletePart(id);
        if (result == Enums.OperationResult.NotFound)
            return NotFound();
        return NoContent();
    }
}