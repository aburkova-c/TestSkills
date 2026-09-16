using System.Net;
using apitest.DTO.BookStoreDTO;
using apitest.Helpers;
using apitest.Interfaces;
using FluentAssertions;
using Refit;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public class BookStoreTests
{
    private IBookStore api;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services
            .AddRefitClient<IBookStore>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://demoqa.com");
            });

        var provider = services.BuildServiceProvider();
        api = provider.GetRequiredService<IBookStore>();
    }

    [Test]
    public async Task CreateUser()
    {
        var user = new UserDTO
        {
            Username = $"Den_{Guid.NewGuid():N}", Password = "StrongPass10!!!"
        };
        var response = await api.CreateUserAsync(user);
    }

    [Test]
    public async Task GetToken()
    {
        try
        {
            await api.CreateUserAsync(new UserDTO
            {
                Username = "Milacek007", Password = "StrongPass123!"
            });
        }
        catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.NotAcceptable)
        {
            // Пользователь уже создан при прошлом запуске теста - это ожидаемо, продолжаем.
        }

        var tokenResponse = await api.GenerateTokenAsync(new LoginRequestDTO
        {
            UserName = "Milacek007",
            Password = "StrongPass123!"
        });

        tokenResponse.Token.Should().NotBeNullOrEmpty();
        tokenResponse.Status.Should().Be("Success");
        tokenResponse.Result.Should().Contain("authorized");
    }
    
    [Test]
    public async Task LoginUser()
    {
        var user = new UserDTO { Username = "Den", Password = "StrongPass10!!!" };
        var response = await api.LoginUserAsync(user);

        response.UserId.Should().NotBeNullOrEmpty();
        response.Username.Should().Contain(user.Username);
    }

    [Test]
    public async Task AddBookAsync()
    {
        var token = await GetTokenAsync();

        var userId = await GetUserIdAsync();
        // isbn 9781449325862
        var newBook = new AddBookRequestDTO
        {
            UserId = userId, CollectionOfIsbns = new List<BookDTO> { new BookDTO { Isbn = "9781449325862" } }
        };

        var addResponse = await api.AddBookAsync(newBook, token);
        addResponse.Books.Should().HaveCount(1);
    }

    [Test]
    public async Task GetAllBooksAsync()
    {
        var response = await api.GetAllBookAsync();

        response.Should().NotBeNull();
        response.Books.Should().HaveCount(8);
    }

    [Test]
    public async Task GetBookByIsbnAsync()
    {
        var getAllBooks = await api.GetAllBookAsync();
        var rndBook = RandomHelper.GetRandomItem(getAllBooks.Books);
        var bookByIsbn = rndBook.Isbn;

        var book = await api.GetBookByIsbnAsync(bookByIsbn);

        book.SubTitle.Should().Be(rndBook.SubTitle);
    }

    [Test]
    public async Task AddMultipleBookAsync()
    {
        var token = await GetTokenAsync();

        var userId = await GetUserIdAsync();
        // isbn 9781449325862
        // isbn 9781449331818
        var newBook = new AddBookRequestDTO
        {
            UserId = userId,
            CollectionOfIsbns = new List<BookDTO>
                { new BookDTO { Isbn = "9781449325862" }, new BookDTO { Isbn = "9781449331818" } }
        };

        var addResponse = await api.AddBookAsync(newBook, token);

        addResponse.Books.Should().HaveCount(2);
    }

    [Test]
    public async Task AddBookWOTokenAsync() //негативный проверка запроса без токена, проверка на 401
    {
        var userId = await GetUserIdAsync();
        var newBook = new AddBookRequestDTO
        {
            UserId = userId,
            CollectionOfIsbns = new List<BookDTO>
                { new BookDTO { Isbn = "9781449325862" }, new BookDTO { Isbn = "9781449331818" } }
        };

        // var addResponse = await api.AddBookAsync(newBook, token: null);
        //
        // addResponse.Books.Should().HaveCount(2);

        Func<Task> action = async () =>
            await api.AddBookAsync(newBook, token: null); //делегат
        await action.Should().ThrowAsync<ApiException>()
            .Where(e => e.StatusCode == System.Net.HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task DeleteBookByIsbnAndByUserIdAsync()
    {
        var token = await GetTokenAsync();
        var userId = await GetUserIdAsync();
        var userInfo = await api.GetUserAsync(userId, token);

        foreach (var book in userInfo.Books)
        {
            var b = new DeleteBookRequestDTO { Isbn = book.Isbn, UserId = userId };
            await api.DeleteBookByIsbnAndByUserIdAsync(b, token);
        }


        var userInfoAfterDeleteBooks = await api.GetUserAsync(userId, token);
        userInfoAfterDeleteBooks.Books.Should().HaveCount(0);
    }


    private async Task<string> GetTokenAsync()
    {
        var login = new LoginRequestDTO { UserName = "Vitaly310826", Password = "Password31082026!" };
        var getToken = await api.GenerateTokenAsync(login);
        var token = $"Bearer {getToken.Token}";
        return token;
    }

    private async Task<string> GetUserIdAsync()
    {
        var user = new UserDTO { Username = "Vitaly310826", Password = "Password31082026!" };
        var response = await api.LoginUserAsync(user);
        var userId = response.UserId;
        return userId;
    }
}