using HotelSystem.Application.Domain.Generic.Generics;
using HotelSystem.Application.Infrastructure.Generics;
using Microsoft.Extensions.DependencyInjection;

namespace HotelSystem.Application.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDependecies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
        return serviceCollection;
    }
}