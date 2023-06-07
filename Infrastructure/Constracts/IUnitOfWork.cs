namespace Infrastructure.Constracts;

public interface IUnitOfWork
{
   void SaveChanges();
   IEmployeeRepository Employee { get; }
}
