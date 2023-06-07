using Api.Error;
using Domain.Entities;
using Infrastructure.Constracts;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constant;

namespace Api.Controllers;

public class EmployeeController : BaseApiController
{
   private readonly IUnitOfWork empRepo;

   public EmployeeController(IUnitOfWork empRepo)
   {
      this.empRepo = empRepo;
   }

   // Employee Create
   //public async Task<IActionResult> Create(TblEmployee model)
   //{
   //   if (model == null) return BadRequest(new ApiResponse(400));

   //   if (await empRepo.Employee.IsDuplicate(e => e.EmployeeCode == model.EmployeeCode) == true)
   //      return BadRequest(new ApiResponse(403, model.EmployeeCode + MessageConstant.DuplicateError));

   //   empRepo.Employee.Add(model);
   //   empRepo.SaveChanges();
   //   return Ok(new ApiResponse(200, MessageConstant.SuccessfullySaved));
   //}

   //// Employee List
   //public async Task<IActionResult> Index()
   //{
   //   IEnumerable<TblEmployee> employees = await empRepo.Employee.GetAllAsync();

   //   if (employees.Count() == 0)
   //      return NotFound(new ApiResponse(404));

   //   return Ok(employees.OrderByDescending(e => e.EmployeeSalary));
   //}

   //// Employee Update
   //public async Task<IActionResult> Update(TblEmployee model)
   //{
   //   if (model == null || model.EmployeeId == 0) 
   //      return BadRequest(new ApiResponse(400));

   //   if (await empRepo.Employee.IsDuplicate(e => e.EmployeeCode == model.EmployeeCode) == true)
   //      return BadRequest(new ApiResponse(403, model.EmployeeCode + MessageConstant.DuplicateError));

   //   empRepo.Employee.Update(model);
   //   empRepo.SaveChanges();
   //   return Ok(new ApiResponse(200, MessageConstant.SuccessfullyUpdated));
   //}

   
}
