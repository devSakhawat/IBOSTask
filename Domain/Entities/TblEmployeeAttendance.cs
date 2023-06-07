using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class TblEmployeeAttendance
{
   public int EmpAttendanceId { get; set; }

   public DateTime AttendanceDate { get; set; }

   public bool IsPresent { get; set; }

   public bool IsAbsent { get; set; }

   public bool IsOffday { get; set; }

   public int EmployeeId { get; set; }
}
