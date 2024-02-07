using ExceptionHandlingInCore.Exceptions;

namespace ExceptionHandlingInCore
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region register service Exception handling
            //You need two things to add an IExceptionHandler implementation to the ASP.NET Core request pipeline:
            //Register the IExceptionHandler service with dependency injection
            //Register the ExceptionHandlerMiddleware with the request pipeline
            //You call the AddExceptionHandler method to register the GlobalExceptionHandler as a service.
            //It's registered with a singleton lifetime.
            //So be careful about injecting services with a different lifetime.
            //I'm also calling AddProblemDetails to generate a Problem Details response for common exceptions.
            builder.Services.AddExceptionHandler<GlobalExecptionHandling>();
            builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
            builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
            builder.Services.AddProblemDetails();
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //call UseExceptionHandler to add
            //the ExceptionHandlerMiddleware to the request pipeline
            app.UseExceptionHandler();
            app.Run();
        }
    }
}