using System.Text.Json;

namespace apitest.Helpers;

public static class JsonFileReader
{
    public static T Read<T>(string fileName)
    {
        var path = Path.Combine(
            AppContext.BaseDirectory,
            "Resources",
            fileName);

        var json = File.ReadAllText(path);

        return JsonSerializer.Deserialize<T>(json)
               ?? throw new InvalidOperationException($"Не удалось прочитать данные из файла: {fileName}");
    }
}