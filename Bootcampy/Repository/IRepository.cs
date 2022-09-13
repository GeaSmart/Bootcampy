using System.Linq.Expressions;

namespace Bootcampy.Repository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<List<T>> ReadAllAsync();
        Task<List<T>> ReadAllAsync(Expression<Func<T, bool>> filter);
        Task<T> ReadOneAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
