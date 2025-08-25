namespace ConsoleApp1
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalPrice => OrderItems.Sum(oi => oi.TotalPrice);
        
        public void AddProduct(Product product, int quantity = 1)
        {
            var existingItem = OrderItems.FirstOrDefault(oi => oi.ProductId == product.Id);
            
            if (existingItem != null)
            {
                // Nếu sản phẩm đã tồn tại, cộng dồn số lượng
                existingItem.Quantity += quantity;
            }
            else
            {
                // Nếu sản phẩm chưa tồn tại, tạo mới OrderItem
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = quantity,
                    UnitPrice = product.Price
                };
                OrderItems.Add(orderItem);
            }
        }
    }
}

