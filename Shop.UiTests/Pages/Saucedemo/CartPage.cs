using Microsoft.Playwright;

namespace Shop.UiTests.Pages.Saucedemo;

public class CartPage
{
    private readonly IPage Page;

    public CartPage(IPage page)
    {
        Page = page;
    }

    public async Task<IReadOnlyList<string>> GetItemNames()
    {
        await Page.Locator(".cart_item .inventory_item_name").First.WaitForAsync();
        return await Page.Locator(".cart_item .inventory_item_name").AllTextContentsAsync();
    }

    public async Task<CheckoutInfoPage> Checkout()
    {
        await Page.GetByRole(AriaRole.Button, new()
        { Name = "Checkout"}).ClickAsync();
        return new CheckoutInfoPage(Page);
    }

    public async Task<string> GetItemPrice(string itemName)
    {
        return await Page.Locator(".cart_item")
            .Filter(new() { HasText = itemName })
            .Locator(".inventory_item_price")
            .TextContentAsync();
    }

}