using Microsoft.Playwright; 

namespace Shop.UiTests.Pages.Saucedemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;
    
    private ILocator CompleteHeader => Page.Locator(".complete-header");
    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }

    public async Task<string> GetConfirmationMessage()
    {
        return await CompleteHeader.TextContentAsync() ?? "";
    }
    
 
    
}