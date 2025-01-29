using DeveloperEvaluation.Application;
using DeveloperEvaluation.Application.Auth.AuthenticateUser;
using DeveloperEvaluation.Common.HealthChecks;
using DeveloperEvaluation.Common.Logging;
using DeveloperEvaluation.Common.Security;
using DeveloperEvaluation.Common.Validation;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.IoC;
using DeveloperEvaluation.ORM;
using DeveloperEvaluation.WebApi.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;
using Serilog;

namespace DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
            builder.Services.AddAutoMapper(typeof(AuthenticateUserProfile));

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddRebus(configure => configure
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "user-events"))
                .Routing(r =>
                {
                    r.TypeBased()
                        .MapAssemblyOf<User>("user-events")
                        .MapAssemblyOf<Product>("product-events");
                }));

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("https://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });


            var app = builder.Build();
            
            
            if (app.Environment.IsDevelopment()) app.UseCors("AllowAll");
            
            app.UseMiddleware<ValidationExceptionMiddleware>();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is ValidationException validationException)
                    {
                        logger.LogWarning(validationException, "Validation failed: {Errors}", validationException.Errors);
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            Success = false,
                            Errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                        });
                        return;
                    }

                    logger.LogError(exceptionHandlerPathFeature?.Error, "Unhandled exception occurred at {Path}", context.Request.Path);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var errorMessage = app.Environment.IsDevelopment() 
                        ? $"An unexpected error occurred. {exceptionHandlerPathFeature?.Error}"
                        : "An unexpected error occurred.";

                    await context.Response.WriteAsJsonAsync(new { Message = errorMessage });
                });
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
