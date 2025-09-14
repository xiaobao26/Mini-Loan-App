using Mini_Loan_App.Controllers.Dtos;
using Mini_Loan_App.Domain;

namespace Mini_Loan_App.Services.Interfaces;

public interface ILoanService
{
    Task<Loan> CreateAsync(CreateLoanRequest request);
    Task<Loan?> GetByIdAsync(Guid id);
}