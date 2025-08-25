namespace ConsoleApp1
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
        
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
