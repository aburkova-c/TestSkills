using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public class TestPreconditions
{
    public ServiceProvider Provider { get; }
    public TestPreconditions()
    {
        var services = new ServiceCollection();
        var dbPath = Path.Combine(AppContext.BaseDirectory, "marketplace.db");
        var connectString = $"Data Source={dbPath};";
        services.AddDataAccess(connectString);
        
        Provider = services.BuildServiceProvider();
    }
}