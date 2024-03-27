using HotelSystem.Application.Domain.Generic.Generics.Repositories;
using HotelSystem.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Application.Infrastructure.Generics.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext _context;

    public Repository(DbContext context) => _context = context;

    public async Task<TEntity> Add(TEntity entity)
    {
        var entityEntry = _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }
    public async Task<IEnumerable<TEntity>> FindAllAsync() => await _context.Set<TEntity>().ToListAsync();
    public async Task<TEntity> FindById<Key>(Key id) where Key :struct => await _context.Set<TEntity>().FindAsync(id);
    public async Task Update(TEntity entity)
    {
        _context.Set<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}