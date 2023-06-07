using Api.Error;
using Infrastructure.Constracts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sql;

namespace Api.Extension;

public static class ApplicationServicesExtensions
{
   public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
   {
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      //services.AddAutoMapper(typeof(MappingProfiles));
      services.AddDbContext<ApplicationDBContext>(op => op.UseSqlServer(config.GetConnectionString("DbLocation")));

      services.Configure<ApiBehaviorOptions>(options =>
      {
         options.InvalidModelStateResponseFactory = actionContext =>
         {
            var errors = actionContext.ModelState
               .Where(e => e.Value.Errors.Count() > 0)
               .SelectMany(x => x.Value.Errors)
               .Select(y => y.ErrorMessage).ToArray();

            var errorResponse = new ApiValidationErrorResponse
            {
               Errors = errors
            };
            return new BadRequestObjectResult(errorResponse);
         };
      });

      services.AddCors(option =>
      {
         option.AddPolicy("CorsPolicy", policy =>
         {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
         });
      });
      return services;
   }
}