using Microsoft.Playwright;                                            
using NUnit.Framework;                                                 
using Shop.UiTests.Fixtures;                                           
using Shop.UiTests.Pages.Heroku;     
using FluentAssertions;

                                                                         
namespace Shop.UiTests;

[TestFixture]
public class HerokuLoginTests : PlaywrightFixture
{
    [Test]
    public async Task FormAuthentication()
    {
        LoginPage loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        await loginPage.LoginUser("wrong-username", "wrong-password");
        var errorMessage = await loginPage.GetTextFromErrorMessageLabelAsync();
        errorMessage.Should().Contain("Your username is invalid!");
    }
}