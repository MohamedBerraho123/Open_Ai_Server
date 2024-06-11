using System.Linq.Expressions;

namespace OpenAI_UIR.Repository.Abstract
{
    public interface IRepository<T> where T : class
    {

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}