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
        return await Page.Locator(".cart_item .inventory_item_name").AllTextContentsAsync();
    }

    public async Task<CheckoutInfoPage> Checkout()
    {
        await Page.GetByRole(AriaRole.Button, new()
        { Name = "Checkout"}).ClickAsync();
        return new CheckoutInfoPage(Page);
    }
}