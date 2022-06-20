using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class ServiceCreateRequest
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
}