using Microsoft.Playwright;

namespace Shop.UiTests.Pages.Saucedemo;

public class ProductsPage
{
    private readonly IPage Page;

    private ILocator ProductCard(string itemName) => Page.Locator(".inventory_item").Filter(new()
        { HasText = itemName });
    private ILocator CartIcon => Page.Locator(".shopping_cart_link");
    
    public ProductsPage(IPage page)                                                                                                                
    {                                                                                                                                              
        Page = page;                                                                                                                               
    }          

    public async Task AddToCart(string itemName)
    {
        await ProductCard(itemName)
            .GetByRole(AriaRole.Button, new() {Name = "Add To Cart"})
            .ClickAsync();
    }
    
    public async Task<CartPage> GoToCart()
    {
        await CartIcon.ClickAsync();
        return new CartPage(Page);
    }

    public async Task<string> GetItemPrice(string itemName)
    { 
        return await ProductCard(itemName)
            .Locator(".inventory_item_price")
            .TextContentAsync();
    }
    
    
}