using apitest.DTO.UserDTO;
using FluentAssertions;
using apitest.Helpers;

namespace apitest;

public class UserTests
{
    private UsersResponseDTO usersResponse;

    [OneTimeSetUp]
    public void Setup()
    {
        usersResponse = JsonFileReader.Read<UsersResponseDTO>("UsersData.json");
    }

// 2.1 Проверить, что количество юзеров из файла равно 10
    [Test]
    public void UsersCount_ShouldBe10()
    {
        var userCount = usersResponse.Data.Count();
        userCount.Should().Be(10);
    }

// 2.2 Проверить, что первый юзер - Alice Johnson
    [Test]
    public void FirstUser_ShouldBeAliceJohnson()
    {
        var firstUser = usersResponse.Data.First();
        firstUser.Profile.FullName.Should().Be("Alice Johnson");
    }

// 2.3 Проверить, что все Id уникальны 

    [Test]
    public void UserIds_ShouldBeUnique()
    {
        var userIds = usersResponse.Data.Select(user => user.Id).ToList();
        userIds.Should().OnlyHaveUniqueItems();
    }

// 2.4 Проверить, что есть хотя бы один премиум-пользователь (тег premium)

    [Test]
    public void Users_ShouldContainPremiumUser()
    {
        var hasPremiumUser = usersResponse.Data.Any(user => user.Profile.Tags.Contains("premium"));
        hasPremiumUser.Should().BeTrue();
    }

// 2.5 Проверить, что у всех юзеров поле город не пустой
    [Test]
    public void UserCities_ShouldNotBeEmpty()
    {
        var allUsersHaveCity = usersResponse.Data.All(user => !string.IsNullOrWhiteSpace(user.Profile.Address.City));
        allUsersHaveCity.Should().BeTrue();
    }

// 2.6 Проверить, что есть хотя бы один пользователь из Стокгольма
    [Test]
    public void Users_ShouldContainUserFromStockholm()
    {
        var usersFromStockholm = usersResponse.Data.Any(user => user.Profile.Address.City == "Stockholm");
        usersFromStockholm.Should().BeTrue();
    }

// 2.7 Проверить, что возраст всех юзеров в диапазоне 18-60 лет 
    [Test]
    public void UserAges_ShouldBeBetween18And60()
    {
        var userAges = usersResponse.Data.All(user => user.Profile.Age >= 18 && user.Profile.Age <= 60);
        userAges.Should().BeTrue();
    }

// 2.8 Проверить, что есть хотя бы один юзер с ролью admin
    [Test]
    public void Users_ShouldContainAdmin()
    {
        var userRoles = usersResponse.Data.Any(user => user.Roles.Contains("admin"));
        userRoles.Should().BeTrue();
    }

// 3. Проверить, что все юзеры (их координаты) находятся в диапазоне Швеции

    [Test]
    public void UserCoordinates_ShouldBeWithinSweden()
    {
        var coordinates = usersResponse.Data
            .Select(user => user.Profile.Address.Geo);

        coordinates.Should().AllSatisfy(geo =>
        {
            geo.Lat.Should().BeInRange(55, 70);
            geo.Lng.Should().BeInRange(10, 25);
        });
    }

// 4.Проверить, что улицы у юзеров соответствуют условиям: содержат номер дома, улица начинается с буквы, улица не состоит только из цифр
    [Test]
    public void UserStreets_ShouldBeValid()
    {
        var userStreets = usersResponse.Data.Select(user => user.Profile.Address.Street);
        userStreets.Should().AllSatisfy(street =>
        {
            street.Should().MatchRegex(@"^\p{L}");
            street.Should().MatchRegex(@"\d");
            street.Should().NotMatchRegex(@"^\d+$");
            street.Should().NotBeNullOrWhiteSpace();
        });
    }
}
// 5. Написать класс-утилиту для чтения файлов

