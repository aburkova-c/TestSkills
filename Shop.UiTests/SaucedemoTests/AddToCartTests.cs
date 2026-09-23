using FluentAssertions;
using Microsoft.Playwright;                                                                                                                
using NUnit.Framework;                                                                                                                     
using Shop.UiTests;
using Shop.UiTests.Pages.Saucedemo;
namespace Shop.UiTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class AddToCartTests : BaseTest
{
    [Test]
    public async Task AddTwoItemsToCart()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.Login("standard_user", "secret_sauce");
        await Expect(Page.GetByText("Products")).ToBeVisibleAsync();
        
        var productsPage = new ProductsPage(Page);
        var backPackPrice = await productsPage.GetItemPrice("Sauce Labs Backpack");
        var backLightPrice = await productsPage.GetItemPrice("Sauce Labs Bike Light");
        
        await productsPage.AddToCart("Sauce Labs Backpack");
        await productsPage.AddToCart("Sauce Labs Bike Light");
        
        var cartPage = await productsPage.GoToCart();
        var itemNames = await cartPage.GetItemNames();
        itemNames.Should().Contain("Sauce Labs Backpack");
        itemNames.Should().Contain("Sauce Labs Bike Light");
        (await cartPage.GetItemPrice("Sauce Labs Backpack")).Should().Be(backPackPrice);
        (await cartPage.GetItemPrice("Sauce Labs Bike Light")).Should().Be(backLightPrice);
    }
}