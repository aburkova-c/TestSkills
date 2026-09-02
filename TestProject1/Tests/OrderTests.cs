using System.Text.Json;
using apitest.DTO.OrderDataDTO;
using apitest.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;

namespace apitest;

public class OrderTests
{
    private OrderDataDTO order;
    
    [OneTimeSetUp]
    public void Setup()
    {
        order = JsonFileReader.Read<OrderDataDTO>("OrderData.json");
    }

    [Test]
    public void Test1() // Тест: Пройти по каждому Item и вывести их значение в лог 
    {
        foreach (var item in order.Items)
        {
            TestContext.WriteLine($"{item.ProductID} | {item.Quantity} | {item.Price}");
        }
        order.Items.Should().NotBeEmpty();
        order.Items.Should().HaveCount(3);
    }

    [Test]
    public void Test2() // Тест: Подсчет общей суммы покупок и сравнит с тоталсум
    {
        var sum = order.Items.Select(x => x.Quantity * x.Price).Sum();
        var expectedSum = order.Summary.ItemsTotal;
        sum.Should().Be(expectedSum);
    }

    [Test]
    public void Test3() // Получение списка items с категорией электроника
    {
        var electronicsItems = order.Items.Where(x => x.Category == "Electronics");
        foreach (var item in electronicsItems)
        {
            TestContext.WriteLine($"Electronics: {item.Name} | {item.Quantity} | {item.Price}");
        }
        electronicsItems.Should().NotBeEmpty();
        electronicsItems.Should().OnlyContain(x => x.Category == "Electronics");
    }

    [Test]
    public void Test4() // у заказа статус оплаты paid 
    {
        order.Payment.Status.Should().Be("paid");
    }

    [Test]
    public void Test5() // выбрать самый дорогой item, сортировка
    {
        var mostExpensiveItem =  order.Items.OrderByDescending(x => x.Price).First();
/*        using (new AssertionScope())
        {
            mostExpensiveItem.Price.Should().Be(129.99m);
            mostExpensiveItem.Name.Should().Be("Wireless Headphones");
        }
*/
        // использование анонимного типа 
        mostExpensiveItem.Should().BeEquivalentTo(new
        {
            Price = 129.99m,
            Name = "Wireless Headphones"
        });
    }

    [Test]
    public void Test6() // цена выше 50
    {
        var electronicsItems = order.Items.Where(x => x.Price > 20m).ToList();
        foreach (var item in electronicsItems)
        {
            TestContext.WriteLine($"Electronics >50: {item.Name} | {item.Quantity} | {item.Price}");
        }
        electronicsItems.Should().NotBeEmpty();
    }
    
}