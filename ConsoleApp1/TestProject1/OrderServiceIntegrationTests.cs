using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1;

[TestClass]
public class OrderServiceIntegrationTests
{
    private ApplicationDbContext _context;
    private OrderService _orderService;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);
        _orderService = new OrderService(_context);
    }

    [TestMethod]
    public void CreateOrder_ShouldSaveOrderToDatabase()
    {
        // Arrange
        var product1 = new Product { Id = 1, Name = "Product A", Price = 10.0m };
        var product2 = new Product { Id = 2, Name = "Product B", Price = 15.0m };
        var order = new Order();
        order.AddProduct(product1, 2);
        order.AddProduct(product2, 1);

        // Act
        _orderService.CreateOrder(order);
        var orders = _orderService.GetOrders();

        // Assert
        Assert.AreEqual(1, orders.Count);
        Assert.AreEqual(35.0m, orders[0].TotalPrice); // (2 * 10) + (1 * 15) = 35
        Assert.AreEqual(2, orders[0].OrderItems.Count);
    }

    [TestMethod]
    public void AddProductToOrder_ShouldUpdateExistingProductQuantity()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product A", Price = 10.0m };
        var order = new Order();
        order.AddProduct(product, 2);
        _orderService.CreateOrder(order);

        // Act
        _orderService.AddProductToOrder(1, product, 3);
        var orders = _orderService.GetOrders();

        // Assert
        Assert.AreEqual(1, orders.Count);
        Assert.AreEqual(1, orders[0].OrderItems.Count); // Chỉ có 1 OrderItem
        Assert.AreEqual(5, orders[0].OrderItems[0].Quantity); // 2 + 3 = 5
        Assert.AreEqual(50.0m, orders[0].TotalPrice); // 5 * 10 = 50
    }

    [TestMethod]
    public void Order_ShouldHandleMultipleProductsWithDifferentQuantities()
    {
        // Arrange
        var product1 = new Product { Id = 1, Name = "Product A", Price = 10.0m };
        var product2 = new Product { Id = 2, Name = "Product B", Price = 15.0m };
        var order = new Order();

        // Act
        order.AddProduct(product1, 3); // 3 * 10 = 30
        order.AddProduct(product2, 2); // 2 * 15 = 30
        order.AddProduct(product1, 1); // Thêm 1 sản phẩm A nữa: 4 * 10 = 40

        // Assert
        Assert.AreEqual(2, order.OrderItems.Count); // 2 loại sản phẩm
        Assert.AreEqual(4, order.OrderItems[0].Quantity); // Product A: 3 + 1 = 4
        Assert.AreEqual(2, order.OrderItems[1].Quantity); // Product B: 2
        Assert.AreEqual(70.0m, order.TotalPrice); // (4 * 10) + (2 * 15) = 70
    }
}
