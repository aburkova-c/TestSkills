using Microsoft.Playwright;                                                                                                                
using NUnit.Framework;                                                                                                                     
using Shop.UiTests;
using Shop.UiTests.Pages.Heroku;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class LoginTestsShop : BaseTest
{
    [Test]
    public async Task Login_WithValidCredentials_OpensOrders()
    {
        await Page.GetByLabel("Username").FillAsync("standard_user");                                                                      
        await Page.GetByLabel("Password").FillAsync("secret_sauce");                                                                       
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" })                                                                    
            .ClickAsync();                                                                                                                 
        await Expect(Page.GetByText("Products"))                                                        
            .ToBeVisibleAsync(); 
    }
}