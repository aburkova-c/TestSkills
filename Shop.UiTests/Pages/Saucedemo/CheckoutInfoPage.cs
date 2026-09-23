using Microsoft.Playwright;

namespace Shop.UiTests.Pages.Saucedemo;

public class CheckoutInfoPage
{
    private readonly IPage Page;
    private ILocator FirstNameTextBoxLocator => Page.GetByPlaceholder("First Name");
    private ILocator LastNameTextBoxLocator => Page.GetByPlaceholder("Last Name");
    private ILocator PostalCodeTextBox => Page.GetByPlaceholder("Zip/Postal Code");

    private ILocator ContinueButtonLocator => Page.GetByRole(AriaRole.Button, new()
    {
        Name = "Continue"
    });
    
    public CheckoutInfoPage(IPage page)
    {
        Page = page;
    }

    public async Task<CheckoutOverviewPage> FillInfoAndContinue(string firstName, string lastName, string postalCode)
    {
        await FirstNameTextBoxLocator.FillAsync(firstName);
        await LastNameTextBoxLocator.FillAsync(lastName);
        await PostalCodeTextBox.FillAsync(postalCode);
        await ContinueButtonLocator.ClickAsync();
            
        return new CheckoutOverviewPage(Page);
    }
}
