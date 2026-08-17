using System.Net;
using apitest.DTO;
using apitest.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace apitest;

public class RefitTests
{
    private IUserApiClient _userApiClient;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddRefitClient<IUserApiClient>()
            .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://reqres.in/api"); });
        var provider = services.BuildServiceProvider();
        _userApiClient = provider.GetRequiredService<IUserApiClient>();
    }

    [Test]
    public async Task Test1()
    {
        var result = await _userApiClient.GetUserAsync(2);
        Assert.Multiple((() =>
        {
            Assert.That(result.Data.Id, Is.EqualTo(2));
            Assert.That(result.Data.Email, Is.Not.Null);
        }));
    }

    [Test]
    public async Task Test2()
    {
        var newUser = new CreateUserRequestDTO()
        {
            Name = "Timur",
            Job = "Programator"
        };
        var response = await _userApiClient.PostUserAsync(newUser);
        Assert.That(response.Name, Is.EqualTo("Timur"));
    }

    [Test]
    public async Task Test3()
    {
        var updateUser = new CreateUserRequestDTO()

    {
            Name = "Timur",
            Job = "Vinař"
        };
        var response = await _userApiClient.PutUserAsync(2, updateUser);
        Assert.That(response.Job, Is.EqualTo("Programator"));
    }

    [Test]
    public async Task Test4()
    {
        var responce =  await _userApiClient.DeleteUserAsync(2);
        Assert.That(responce.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}