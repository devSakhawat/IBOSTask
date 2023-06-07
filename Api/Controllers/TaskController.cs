using Api.Error;
using Domain.Entities;
using Infrastructure.Constracts;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constant;

namespace Api.Controllers
{
   public class TaskController : BaseApiController
   {
      private readonly IUnitOfWork context;

      public TaskController(IUnitOfWork context)
      {
         this.context = context;
      }

      // API01# Update an employee’s Employee Code [Don't allow duplicate employee code]
      [Route(RouteConstant.UpdateEmployee)]
      [HttpPut]
      public async Task<IActionResult> UpdateEmployee(int id, TblEmployee model)
      {
         if (id != model.EmployeeId)
            return BadRequest(new ApiResponse(400));

         if (await context.Employee.IsDuplicate(e => e.EmployeeCode == model.EmployeeCode) == true)
            return BadRequest(new ApiResponse(403, model.EmployeeCode + MessageConstant.DuplicateError));

         TblEmployee employeeObj = await context.Employee.GetFirstOrDefaultAsync(e => e.EmployeeId == id);

         employeeObj.EmployeeId = model.EmployeeId;
         employeeObj.EmployeeName = model.EmployeeName;
         employeeObj.EmployeeCode = model.EmployeeCode;
         employeeObj.EmployeeSalary = model.EmployeeSalary;

         context.Employee.Update(employeeObj);
         context.SaveChanges();
         return Ok(new ApiResponse(200, MessageConstant.SuccessfullyUpdated));
      }

      //API02# Get all employee based on maximum to minimum salary
      [Route(RouteConstant.EmployeesSalaryDesc)]
      [HttpGet]
      public async Task<IActionResult> GetAllEmployee()
      {
         IEnumerable<TblEmployee> employees = await context.Employee.GetAllAsync();

         if (employees.Count() == 0)
            return NotFound(new ApiResponse(404));

         return Ok(employees.OrderByDescending(e => e.EmployeeSalary));
      }

      //API03# Find all employee who is absent at least one day
      [Route(RouteConstant.AbsentEmployees)]
      [HttpGet]
      public async Task<IActionResult> AbsentEmployees()
      {
         IEnumerable<TblEmployee> absentEmployees = await context.Employee.AbsentEmployees();

         if (absentEmployees.Count() == 0)
            return NotFound(new ApiResponse(404));

         return Ok(absentEmployees);
      }

      //API04# Get monthly attendance report of all employee
      [Route(RouteConstant.EmployeeAttendanceReport)]
      [HttpGet]
      public IActionResult EmployeeAttendanceReport()
      {
         var employeeAttendanceReport = context.Employee.EmployeeAttendanceReport();

         if (employeeAttendanceReport == null)
            return NotFound(new ApiResponse(404));

         return Ok(employeeAttendanceReport);
      }
   }
}