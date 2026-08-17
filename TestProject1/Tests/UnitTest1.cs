using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using apitest.DTO;

namespace apitest;
public class Tests
{
    private static HttpClient client;
    //работает только для тестов в классе Test
    [OneTimeSetUp]
    public void Setup()
    {
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://reqres.in/api/")
        };
        client.DefaultRequestHeaders.Add("x-api-key", "free_user_3HrWsxg43Ph37efyTBEFS2tnWPu");
    }

    [Test]
    public async Task Test1()
    { 
        //Get запрос
        using HttpResponseMessage response = await client.GetAsync("users/2");
        //проверка статускода
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task Test2()
    {
        using HttpResponseMessage response = await client.GetAsync("users/2");
        string jsonGet = await response.Content.ReadAsStringAsync();
        UserResponseDTO userResponce = JsonSerializer.Deserialize<UserResponseDTO>(jsonGet);
        UserDataDTO user = userResponce.Data;
        if (user.Id == 2)
        {
        
        }
        else
        {
            throw new Exception();
        }
        
    }   
    
    [Test]
    public async Task Test3()
    {
        // создать имя и название компании, где работает юзер
        var newUser = new CreateUserRequestDTO
        {
            Name = "Alena",
            Job = "Evernode"
        };
        using HttpResponseMessage response = await client.PostAsJsonAsync("users", newUser);
        string jsonPost = await response.Content.ReadAsStringAsync();
        CreateUserResponseDTO createdUser = JsonSerializer.Deserialize<CreateUserResponseDTO>(jsonPost);
    }

    [Test]
    public async Task Test4()
    {
        var newUser = new CreateUserRequestDTO
        {
            Name = "Alena",
            Job = "Second hand"
        };
        using HttpResponseMessage response = await client.PutAsJsonAsync("users/2", newUser);
        string jsonPut = await response.Content.ReadAsStringAsync();
        CreateUserRequestDTO updatedUser = JsonSerializer.Deserialize<CreateUserRequestDTO>(jsonPut);
        response.EnsureSuccessStatusCode();
    }
    
    [Test]
    public async Task Test5()
    { 
        //Delete запрос
        using HttpResponseMessage response = await client.DeleteAsync("users/2");
        //проверка статускода
        response.EnsureSuccessStatusCode();
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        client.Dispose();
    }
    
    
}

//free_user_3HpELMQtbGQTO17ItccFNl2nZss