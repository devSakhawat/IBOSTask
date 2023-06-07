using Domain.DTOs;
using Domain.Entities;

namespace Infrastructure.Constracts;

public interface IEmployeeRepository : IGenericRepository<TblEmployee>
{
   Task<IEnumerable<TblEmployee>> AbsentEmployees();
   List<EmployeeReportDto> EmployeeAttendanceReport();
}
