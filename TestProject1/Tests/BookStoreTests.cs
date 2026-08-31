using System.Net;
using apitest.DTO.BookStoreDTO;
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
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
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
}