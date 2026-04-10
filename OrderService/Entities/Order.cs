namespace OrderService.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";

    // This allows you to do: myOrder.Items.Add(newItem);
    public List<OrderItem> Items { get; set; } = new();
}
