using Microsoft.Playwright;

namespace Shop.UiTests.Pages.Saucedemo;

public class CheckoutOverviewPage
{
    private readonly IPage Page;

    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new()
        { Name = "Finish" });

    public CheckoutOverviewPage(IPage page)
    {
        Page = page;
    }

    public async Task<IReadOnlyList<string>> GetItemNames()
    {
        await Page.Locator(".cart_item .inventory_item_name").First.WaitForAsync();
        return await Page.Locator(".cart_item .inventory_item_name").AllTextContentsAsync();
    }

    public async Task<CheckoutCompletePage> Finish()
    {
        await FinishButton.ClickAsync();
        return new CheckoutCompletePage(Page);
    }
    
    public async Task<string> GetItemPrice(string itemName)
    {
        return await Page.Locator(".cart_item")
            .Filter(new() { HasText = itemName })
            .Locator(".inventory_item_price")
            .TextContentAsync();
    }

}