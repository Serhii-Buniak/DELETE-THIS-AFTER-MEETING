namespace WebApplication1.Models;

public partial class Order
{
    public long Id { get; set; }

    public decimal TotalPrice { get; set; }

    public long ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public long ServiceId { get; set; }
    public Service Service { get; set; } = null!;
}
