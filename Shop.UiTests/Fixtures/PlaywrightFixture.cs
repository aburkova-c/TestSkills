using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;                                                                                                                
using NUnit.Framework;                                                                                                                     
using Shop.UiTests.Fixtures;                                                                                                               


namespace Shop.UiTests.Fixtures;

public class PlaywrightFixture : PageTest
{
    public override BrowserNewContextOptions ContextOptions()                                                                                  
    {                                                                                                                                          
        return new BrowserNewContextOptions                                                                                                    
        {                                                                                                                                      
            ViewportSize = new ViewportSize { Width = 1280, Height = 720 },                                                                    
            IgnoreHTTPSErrors = true,                                                                                                          
        };                                                                                                                                     
    }                                                                                                                                          

}