using System.Linq.Expressions;

namespace Infrastructure.Constracts;

public interface IGenericRepository<T> where T : class
{
   Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
   Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
   Task<bool> IsDuplicate(Expression<Func<T, bool>> predicate);
   void Update(T entity);
}