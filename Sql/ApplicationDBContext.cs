using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sql;

public partial class ApplicationDBContext : DbContext
{
   public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
       : base(options)
   {
   }

   public virtual DbSet<TblEmployee> TblEmployees { get; set; }

   public virtual DbSet<TblEmployeeAttendance> TblEmployeeAttendances { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<TblEmployee>(entity =>
      {
         entity.HasKey(e => e.EmployeeId);

         entity.ToTable("tblEmployee");

         entity.Property(e => e.EmployeeId)
             .ValueGeneratedNever()
             .HasColumnName("employeeId");
         entity.Property(e => e.EmployeeCode).HasColumnName("employeeCode");
         entity.Property(e => e.EmployeeName).HasColumnName("employeeName");
         entity.Property(e => e.EmployeeSalary).HasColumnName("employeeSalary");
      });

      modelBuilder.Entity<TblEmployeeAttendance>(entity =>
      {
         entity.HasKey(e => e.EmpAttendanceId).HasName("PK_tblEmployeeAttendances");

         entity.ToTable("tblEmployeeAttendance");

         entity.Property(e => e.EmpAttendanceId).HasColumnName("empAttendanceId");
         entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

         //entity.HasOne(d => d.Employee).WithMany(p => p.TblEmployeeAttendances)
         //    .HasForeignKey(d => d.EmployeeId)
         //    .OnDelete(DeleteBehavior.ClientSetNull)
         //    .HasConstraintName("FK_tblEmployeeAttendance_tblEmployee");
      });

      OnModelCreatingPartial(modelBuilder);
   }

   partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}