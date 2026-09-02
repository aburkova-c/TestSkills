using apitest.Helpers;
using apitest.Interfaces.PetStore;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace apitest;

public class PetTests
{
    public IPetIP PetAPI;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
        };
        services.AddRefitClient<IPetIP>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://petstoreapi.com/v1");
            })
            .ConfigurePrimaryHttpMessageHandler(() => handler);
        var provider = services.BuildServiceProvider();
        PetAPI = provider.GetRequiredService<IPetIP>();
    }
    
    [Test]
    public async Task GetAllPetsAsync()
    {
        var pets = await PetAPI.GetAllPetsAsync();
        pets.Data.Should().HaveCount(20);
    }

    [Test]
    public async Task GetPetByIDAsync()
    {
        var pets = await PetAPI.GetAllPetsAsync();
        pets.Data.Should().HaveCount(20);

        var rndId = RandomHelper.GetRandomItem(pets.Data).Id;
        var pet = await PetAPI.GetPetByIdAsync(rndId);
        var pet2 = pets.Data.Where(p => p.Id == rndId).ToList();
        
        pet.AgeMonths.Should().Be(pet2[0].AgeMonths);
        pet.Should().NotBeNull();
    }

    [Test]
    public async Task GetAllPetsByStatusAndLimitAsync()
    {
        var pets = await PetAPI.GetAllPetsByStatusAndLimitAsync("ADOPTED", 12);
        pets.Data.Should().HaveCount(12);
    }
}