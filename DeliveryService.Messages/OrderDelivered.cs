namespace DeliveryService.Messages
{
    public class OrderDelivered
    {
        public Guid OrderId { get; }

        public OrderDelivered(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
