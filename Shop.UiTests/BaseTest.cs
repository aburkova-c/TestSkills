using Microsoft.Playwright;                                                                                                                
using NUnit.Framework;                                                                                                                     
using Shop.UiTests.Fixtures;                                                                                                               
                                                                                                                                             
namespace Shop.UiTests;      
                                                                                                                                          
public class BaseTest : PlaywrightFixture      
    {                 
        protected const string BaseUrl = "https://www.saucedemo.com/";
        
        [SetUp]
        public async Task NavigateToSite()
            {
                await Page.GotoAsync(BaseUrl);
            } 
    }                     