namespace WebApplication1.Models;

public partial class Service
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}