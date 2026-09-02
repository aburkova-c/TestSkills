using apitest.Interfaces.DapperInterfaces;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public class DapperTest
{
    private TestPreconditions _preconditions;

    [OneTimeSetUp]
    public async Task Setup()
    {
        var dbPath = Path.Combine(AppContext.BaseDirectory, "marketplace.db");
        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        }

        var connectionString = $"Data Source={dbPath};";
        await using (var connection = new SqliteConnection(connectionString))
        {
            await connection.OpenAsync();
            await DatabaseInitializer.InitializeAsync(connection);
        }

        _preconditions = new TestPreconditions();
    }

    [Test]
    public async Task GetAllUsers()
    {
        var repo = _preconditions.Provider.GetRequiredService<IUserRepository>();
        var users = await repo.GetAllAsync();
        users.Should().HaveCount(15);
    }

    [Test]
    public async Task GetAllCategories()
    {
        var repo = _preconditions.Provider.GetRequiredService<ICategoryRepository>();
        var categories = await repo.GetAllAsync();
        categories.Should().HaveCount(6);
    }

    [Test]
    public async Task GetProductById()
    {
        var repo = _preconditions.Provider.GetRequiredService<IProductRepository>();
        var product = await repo.GetByIdAsync(1);

        product.Should().NotBeNull();
        product!.Id.Should().Be(1);
        product.Name.Should().Be("iPhone 15");
        product.Description.Should().Be("Смартфон Apple");
        product.Price.Should().Be(79990);
        product.Stock.Should().Be(15);
        product.CategoryId.Should().Be(1);
    }

    [Test]
    public async Task GetOrderWithItemsForUser()
    {
        var orderRepo = _preconditions.Provider.GetRequiredService<IOrderRepository>();
        var orderItemRepo = _preconditions.Provider.GetRequiredService<IOrderItemRepository>();
        var productRepo = _preconditions.Provider.GetRequiredService<IProductRepository>();

        var order = await orderRepo.GetByIdAsync(1);
        order.Should().NotBeNull();
        order!.UserId.Should().Be(1);

        var items = (await orderItemRepo.GetByOrderIdAsync(order.Id)).ToList();

        var expectedItems = new[]
        {
            new { ProductId = 1, Quantity = 1, UnitPrice = 79990m },
            new { ProductId = 15, Quantity = 1, UnitPrice = 4990m }
        };

        items.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice })
            .Should().BeEquivalentTo(expectedItems);

        var productNames = new List<string>();
        foreach (var item in items)
        {
            var product = await productRepo.GetByIdAsync(item.ProductId);
            product.Should().NotBeNull();
            productNames.Add(product!.Name);
        }

        productNames.Should().BeEquivalentTo(new[] { "iPhone 15", "Anker PowerBank" });
    }
}