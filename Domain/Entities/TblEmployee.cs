using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class TblEmployee
{
   public int EmployeeId { get; set; }

   public string EmployeeName { get; set; } = null!;

   public string EmployeeCode { get; set; } = null!;

   public double EmployeeSalary { get; set; }

}
