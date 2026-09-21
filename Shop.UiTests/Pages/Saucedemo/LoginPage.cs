using Microsoft.Playwright;

namespace Shop.UiTests.Pages.Saucedemo;

public class LoginPage
{
    private readonly IPage Page;
    private ILocator UsernameTextBox => Page.GetByLabel("Username");
    private ILocator PasswordTextBox => Page.GetByLabel("Password");
    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() {Name = "Login"});
    
    public LoginPage(IPage page)
        {
        Page = page;
        }

    public async Task Login(string username, string password)
    {
        await UsernameTextBox.FillAsync(username);
        await PasswordTextBox.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}