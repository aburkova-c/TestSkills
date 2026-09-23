using Shop.UiTests.Pages.Saucedemo;
using FluentAssertions; 
using Microsoft.Playwright;                                                                                                                
using NUnit.Framework;                                                                                                                     
using Shop.UiTests;

namespace Shop.UiTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class CheckoutTests : BaseTest
{
    [Test]
    public async Task FullCheckOutFlow()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.Login("standard_user", "secret_sauce");
        await Expect(Page.GetByText("Products")).ToBeVisibleAsync();
        
        var productsPage = new ProductsPage(Page);
        await productsPage.AddToCart("Sauce Labs Backpack");                         
        await productsPage.AddToCart("Sauce Labs Bike Light");
        var backPackPrice = await productsPage.GetItemPrice("Sauce Labs Backpack");
        var backLightPrice = await productsPage.GetItemPrice("Sauce Labs Bike Light");


        var cartPage = await productsPage.GoToCart();
        var itemsInCart = await cartPage.GetItemNames();
        itemsInCart.Should().Contain("Sauce Labs Backpack");
        itemsInCart.Should().Contain("Sauce Labs Bike Light");
        (await cartPage.GetItemPrice("Sauce Labs Backpack")).Should().Be(backPackPrice);
        (await cartPage.GetItemPrice("Sauce Labs Bike Light")).Should().Be(backLightPrice);
        
        var checkoutInfoPage = await cartPage.Checkout();
        var checkoutOverviewPage = await checkoutInfoPage.FillInfoAndContinue("Amina",
            "Nova", "12345");
        
        var itemsInOverview = await checkoutOverviewPage.GetItemNames();
        itemsInOverview.Should().Contain("Sauce Labs Backpack");
        itemsInOverview.Should().Contain("Sauce Labs Bike Light");
        (await checkoutOverviewPage.GetItemPrice("Sauce Labs Backpack")).Should().Be(backPackPrice);
        (await checkoutOverviewPage.GetItemPrice("Sauce Labs Bike Light")).Should().Be(backLightPrice);
        
        
        var checkoutCompletePage = await checkoutOverviewPage.Finish();
        var message = await checkoutCompletePage.GetConfirmationMessage();
        message.Should().Be("Thank you for your order!");

    }
}