using System.Linq.Expressions;

namespace LibraryManagementSystem.Repositories.generic;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(object id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task SaveChangesAsync();
}