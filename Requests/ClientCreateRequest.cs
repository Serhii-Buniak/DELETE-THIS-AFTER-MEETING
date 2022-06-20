using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class ClientCreateRequest
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
}