using Infrastructure.Constracts;
using Microsoft.EntityFrameworkCore;
using Sql;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
   private readonly ApplicationDBContext context;
   internal DbSet<T> dbSet;

   public GenericRepository(ApplicationDBContext context)
   {
      this.context = context;
      dbSet = context.Set<T>();
   }

   public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
   {
      IQueryable<T> query = dbSet;
      return await query.Where(filter).FirstOrDefaultAsync();
   }

   public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
   {
      IQueryable<T> query = dbSet;
      if (filter != null)
      {
         query = query.Where(filter);
      }
     
      return await query.AsNoTracking().ToListAsync();
   }

   public void Update(T entity)
   {
      dbSet.Attach(entity);
      context.Entry(entity).State = EntityState.Modified;
   }

   public async Task<bool> IsDuplicate(Expression<Func<T, bool>> predicate)
   {
      var checkDuplicate = await dbSet.Where(predicate).FirstOrDefaultAsync();

     return (checkDuplicate != null) ? true : false;
   }
}