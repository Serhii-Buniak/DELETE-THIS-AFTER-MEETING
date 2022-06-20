namespace WebApplication1.Models;

public partial class Client
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
