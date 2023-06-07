using Infrastructure.Constracts;
using Sql;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
   private readonly ApplicationDBContext context;

   public UnitOfWork(ApplicationDBContext context)
   {
      this.context = context;
      Employee = new EmployeeRepository(context);
   }

   public void SaveChanges()
   {
      context.SaveChanges();
   }

   public IEmployeeRepository Employee { get; private set; }
}
