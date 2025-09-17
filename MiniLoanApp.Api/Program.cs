using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mini_Loan_App.Infrastructure;
using Mini_Loan_App.Infrastructure.Messaging;
using Mini_Loan_App.Infrastructure.Repositories;
using Mini_Loan_App.Middlewares;
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
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });

            // Define the API Key security scheme
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "API Key authentication using the 'X-API-Key' header",
                Name = "X-API-Key", // The name of the header where the API key will be sent
                In = ParameterLocation.Header, // Location of the API key (Header, Query, Cookie)
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKeyScheme" // Arbitrary name for the scheme
            });

            // Add a security requirement to apply the API Key to all operations
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        },
                        Scheme = "oauth2",
                        Name = "ApiKey",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        var app = builder.Build();
        app.UseMiddleware<ApiKeyMiddleware>();
        // app.UseMiddleware<ExceptionHandlingMiddleware>();
        // app.UseMiddleware<RequestLoggingMiddleware>();
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        app.MapControllers();
        app.Run();
    }
}