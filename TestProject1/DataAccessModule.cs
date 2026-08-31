using apitest.Interfaces.DapperInterfaces;
using apitest.DapperRepository;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public static class DataAccessModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository>(p => new UserRepository(connectionString));
        services.AddScoped<IAddressRepository>(p => new AddressRepository(connectionString));
        services.AddScoped<ICategoryRepository>(p => new CategoryRepository(connectionString));
        services.AddScoped<IProductRepository>(p => new ProductRepository(connectionString));
        services.AddScoped<IOrderRepository>(p => new OrderRepository(connectionString));
        services.AddScoped<IOrderItemRepository>(p => new OrderItemRepository(connectionString));
        services.AddScoped<IReviewRepository>(p => new ReviewRepository(connectionString));
        return services;
    }
}