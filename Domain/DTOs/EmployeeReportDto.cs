using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class EmployeeReportDto
{
   [Display(Name = "Employee Name")]
   public string EmployeeName { get; set; }
   
   [Display(Name = "Month Name")]
   public string MonthName { get; set; }
   
   [Display(Name = "Total Present")]
   public int TotalPresent { get; set; }
   
   [Display(Name = "Total Absent")]
   public int TotalAbsent { get; set; }
   
   [Display(Name = "Total Offday")]
   public int TotalOffday { get; set; }
}