using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Controller;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly DryCleaningContext _context;

    public ClientController(DryCleaningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _context.Clients.ToListAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ClientCreateRequest request)
    {
        Client client = new()
        {
            LastName = request.LastName,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
        };

        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return Created(nameof(GetById), client.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")] 
    public async Task<IActionResult> Patch(long id, [FromBody] JsonPatchDocument<Client> patchDoc)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(client);
        _context.Attach(client);
        await _context.SaveChangesAsync();
        return Ok();
    }
}