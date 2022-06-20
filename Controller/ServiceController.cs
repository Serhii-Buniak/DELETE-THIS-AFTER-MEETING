using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Controller;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly DryCleaningContext _context;

    public ServiceController(DryCleaningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await _context.Services.ToListAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var service = await _context.Services.FindAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        return Ok(service);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ServiceCreateRequest request)
    {
        Service service = new()
        {
            Name = request.Name,
            Price = request.Price
        };

        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
        return Created(nameof(GetById), service.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var service = await _context.Services.FindAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")] 
    public async Task<IActionResult> Patch(long id, [FromBody] JsonPatchDocument<Service> patchDoc)
    {
        var service = await _context.Services.FindAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(service);
        _context.Attach(service);
        await _context.SaveChangesAsync();
        return Ok();
    }
}