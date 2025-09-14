using Microsoft.EntityFrameworkCore;
using Mini_Loan_App.Infrastructure;
using Mini_Loan_App.Infrastructure.Messaging;
using Mini_Loan_App.Infrastructure.Repositories;
using Mini_Loan_App.Services;
using Mini_Loan_App.Services.Interfaces;

namespace Mini_Loan_App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Register DbContext to Container
        builder.Services.AddDbContext<LoanDbContext>(opt =>
        {
            opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        // Register Controllers
        builder.Services.AddControllers();
        
        // Add services to the container.
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<ILoanService, LoanService>();
        builder.Services.AddScoped<IApprovalPolicy, ApprovalPolicy>();
        builder.Services.AddScoped<IAmortizationService, AmortizationService>();

        builder.Services.AddSingleton<ILoanTopicPublisher>(sp =>
        {
            var connectionString = builder.Configuration.GetConnectionString("ServiceBusConnection");
            return new LoanTopicPublisher(connectionString);
        });
        
        
        builder.Services.AddAuthorization();

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        app.MapControllers();
        app.Run();
    }
}