using HotelSystem.Application.Domain.Generic.Generics.Repositories;
using HotelSystem.Application.Entities;

namespace HotelSystem.Application.Domain.Generic.Generics;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity>? Repository<TEntity>() where TEntity : Entity;
}