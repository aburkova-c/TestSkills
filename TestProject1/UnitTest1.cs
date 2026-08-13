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
        client.DefaultRequestHeaders.Add("x-api-key", "free_user_3HpELMQtbGQTO17ItccFNl2nZss");
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
    
    [OneTimeTearDown]
    public void TearDown()
    {
        client.Dispose();
    }
}

//free_user_3HpELMQtbGQTO17ItccFNl2nZss