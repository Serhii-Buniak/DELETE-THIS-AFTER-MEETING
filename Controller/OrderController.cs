using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Controller;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly DryCleaningContext _context;

    public OrderController(DryCleaningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _context.Orders.ToListAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateRequest request)
    {
        Order order = new()
        {
            TotalPrice = request.TotalPrice,
            ClientId = request.ClientId,
            ServiceId = request.ServiceId,
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return Created(nameof(GetById), order.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")] 
    public async Task<IActionResult> Patch(long id, [FromBody] JsonPatchDocument<Order> patchDoc)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(order);
        _context.Attach(order);
        await _context.SaveChangesAsync();
        return Ok();
    }
}