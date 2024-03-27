using System.Collections;
using HotelSystem.Application.Domain.Generic.Generics;
using HotelSystem.Application.Domain.Generic.Generics.Repositories;
using HotelSystem.Application.Entities;
using HotelSystem.Application.Infrastructure.Configurations;
using HotelSystem.Application.Infrastructure.Generics.Repositories;
using Microsoft.Extensions.Configuration;

namespace HotelSystem.Application.Infrastructure.Generics;

public class UnitOfWork(ContextBase context, IConfiguration configuration) : IUnitOfWork
{
    private Hashtable _repositories;

    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }

    public IRepository<TEntity>? Repository<TEntity>() where TEntity : Entity
    {
        var isFileSystem = configuration.GetSection("DATA_TYPE").Value == "FS";
        if (_repositories == null || isFileSystem)
            _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type)) return _repositories[type] as IRepository<TEntity>;
        var repositoryType = isFileSystem ? typeof(FileSystemRepository<>) : typeof(Repository<>);
        if (repositoryType == typeof(Repository<>))
        {
            var repositoryInstance =
                Activator.CreateInstance(repositoryType
                    .MakeGenericType(typeof(TEntity)), context);

            _repositories.Add(type, repositoryInstance);
        }
        else
        {
            var repositoryInstance =
                Activator.CreateInstance(repositoryType
                    .MakeGenericType(typeof(TEntity)),configuration);
            _repositories.Add(type, repositoryInstance);
        }
        return _repositories[type] as IRepository<TEntity>;
    }
    public void Dispose()
    {
        context.Dispose();
    }
}