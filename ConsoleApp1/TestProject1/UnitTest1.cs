using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;
using ConsoleApp1;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class OrderServiceTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private OrderService _orderService;
    private Mock<DbSet<Order>> _mockOrdersDbSet;
    private Mock<DbSet<Product>> _mockProductsDbSet;
    private Mock<DbSet<OrderItem>> _mockOrderItemsDbSet;

    [TestInitialize]
    public void Setup()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _mockOrdersDbSet = new Mock<DbSet<Order>>();
        _mockProductsDbSet = new Mock<DbSet<Product>>();
        _mockOrderItemsDbSet = new Mock<DbSet<OrderItem>>();

        _mockContext.Setup(c => c.Orders).Returns(_mockOrdersDbSet.Object);
        _mockContext.Setup(c => c.Products).Returns(_mockProductsDbSet.Object);
        _mockContext.Setup(c => c.OrderItems).Returns(_mockOrderItemsDbSet.Object);

        _orderService = new OrderService(_mockContext.Object);
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

        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        // Act
        _orderService.CreateOrder(order);

        // Assert
        _mockOrdersDbSet.Verify(d => d.Add(It.IsAny<Order>()), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void GetOrders_ShouldReturnAllOrders()
    {
        // Arrange
        var orders = new List<Order>
        {
            new Order { Id = 1 },
            new Order { Id = 2 }
        }.AsQueryable();

        var mockOrdersDbSet = new Mock<DbSet<Order>>();
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

        _mockContext.Setup(c => c.Orders).Returns(mockOrdersDbSet.Object);

        // Act
        var result = _orderService.GetOrders();

        // Assert
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void AddProductToOrder_ShouldAddNewProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product A", Price = 10.0m };
        var order = new Order { Id = 1 };
        var orders = new List<Order> { order }.AsQueryable();

        var mockOrdersDbSet = new Mock<DbSet<Order>>();
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

        _mockContext.Setup(c => c.Orders).Returns(mockOrdersDbSet.Object);
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        // Act
        _orderService.AddProductToOrder(1, product, 2);

        // Assert
        Assert.AreEqual(1, order.OrderItems.Count);
        Assert.AreEqual(2, order.OrderItems[0].Quantity);
        Assert.AreEqual(10.0m, order.OrderItems[0].UnitPrice);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void AddProductToOrder_ShouldUpdateExistingProductQuantity()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product A", Price = 10.0m };
        var order = new Order { Id = 1 };
        order.AddProduct(product, 2); // Thêm sản phẩm lần đầu với số lượng 2
        
        var orders = new List<Order> { order }.AsQueryable();

        var mockOrdersDbSet = new Mock<DbSet<Order>>();
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
        mockOrdersDbSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

        _mockContext.Setup(c => c.Orders).Returns(mockOrdersDbSet.Object);
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        // Act
        _orderService.AddProductToOrder(1, product, 3); // Thêm thêm 3 sản phẩm

        // Assert
        Assert.AreEqual(1, order.OrderItems.Count);
        Assert.AreEqual(5, order.OrderItems[0].Quantity); // 2 + 3 = 5
        Assert.AreEqual(50.0m, order.TotalPrice); // 5 * 10 = 50
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void Order_TotalPrice_ShouldCalculateCorrectly()
    {
        // Arrange
        var product1 = new Product { Id = 1, Name = "Product A", Price = 10.0m };
        var product2 = new Product { Id = 2, Name = "Product B", Price = 15.0m };
        var order = new Order();

        // Act
        order.AddProduct(product1, 2); // 2 * 10 = 20
        order.AddProduct(product2, 1); // 1 * 15 = 15

        // Assert
        Assert.AreEqual(35.0m, order.TotalPrice);
    }

    [TestMethod]
    public void Order_AddProduct_ShouldHandleDuplicateProducts()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product A", Price = 10.0m };
        var order = new Order();

        // Act
        order.AddProduct(product, 2);
        order.AddProduct(product, 3); // Thêm cùng sản phẩm với số lượng khác

        // Assert
        Assert.AreEqual(1, order.OrderItems.Count); // Chỉ có 1 OrderItem
        Assert.AreEqual(5, order.OrderItems[0].Quantity); // 2 + 3 = 5
        Assert.AreEqual(50.0m, order.TotalPrice); // 5 * 10 = 50
    }
}