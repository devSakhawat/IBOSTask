using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Constracts;
using Microsoft.EntityFrameworkCore;
using Sql;
using System.Globalization;

namespace Infrastructure.Repositories;

public class EmployeeRepository : GenericRepository<TblEmployee>, IEmployeeRepository
{
   private readonly ApplicationDBContext context;

   public EmployeeRepository(ApplicationDBContext context) : base(context)
   {
      this.context = context;
   }
   public async Task<IEnumerable<TblEmployee>> AbsentEmployees()
   {
      var absentEmployees = await context.TblEmployeeAttendances
                          .Where(a => a.IsAbsent)
                          .GroupBy(a => a.EmployeeId)
                          .Where(g => g.Count() >= 1)
                          .Select(g => g.Key)
                          .ToListAsync();

      var employees = await context.TblEmployees.Where(e => absentEmployees.Contains(e.EmployeeId)).ToListAsync();

      return employees;
   }


   public List<EmployeeReportDto> EmployeeAttendanceReport()
   {
      var attendanceReport = (from attendance in context.TblEmployeeAttendances
                              group attendance by new { attendance.EmployeeId, attendance.AttendanceDate.Month } into g
                              select new
                              {
                                 EmployeeId = g.Key.EmployeeId,
                                 MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                                 TotalPresent = g.Count(a => a.IsPresent),
                                 TotalAbsent = g.Count(a => a.IsAbsent),
                                 TotalOffday = g.Count(a => a.IsOffday)
                              }).ToList();

      var employeeAttendance = (from attendance in attendanceReport
                                join employee in context.TblEmployees
                                on attendance.EmployeeId equals employee.EmployeeId
                                select new
                                {
                                   employee.EmployeeName,
                                   attendance.MonthName,
                                   attendance.TotalPresent,
                                   attendance.TotalAbsent,
                                   attendance.TotalOffday,
                                }).ToList();
      List<EmployeeReportDto> employeeAttendanceReport = new List<EmployeeReportDto>();
      if (employeeAttendance.Count > 0)
      {
         foreach (var item in employeeAttendance)
         {
            employeeAttendanceReport.Add(new EmployeeReportDto { EmployeeName = item.EmployeeName, MonthName = item.MonthName, TotalPresent = item.TotalPresent, TotalAbsent = item.TotalAbsent, TotalOffday = item.TotalOffday });
         }
         return employeeAttendanceReport;
      }
      return new List<EmployeeReportDto>();
   }
}
