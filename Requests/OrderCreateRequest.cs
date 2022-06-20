using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class OrderCreateRequest
{
    [Required]
    public decimal TotalPrice { get; set; }
    [Required]
    public long ClientId { get; set; }
    [Required]
    public long ServiceId { get; set; }
}