namespace HotelSystem.Application.Domain.Generic.Generics.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> FindById<Key>(Key id) where Key : struct;
    Task<TEntity> Add(TEntity entity);
    Task<IEnumerable<TEntity>> FindAllAsync();
    Task Update(TEntity entity);
}