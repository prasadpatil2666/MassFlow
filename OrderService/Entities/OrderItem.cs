namespace OrderService.Entities;

public class OrderItem
{
    // 1. Primary Key (Required for EF Core)
    public int Id { get; set; }

    public Guid ArticleId { get; set; }

    // Fixed typo: ArticleTitle
    public string ArticleTitle { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    // 2. Foreign Key (Links this item to an Order)
    public Guid OrderId { get; set; }

    // 3. Calculated Property
    // Note: EF Core will ignore this for the database by default 
    // because it doesn't have a 'set;'. This is correct.
    public decimal PriceTotal => this.Price * this.Quantity;
}
