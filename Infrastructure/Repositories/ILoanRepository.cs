using Mini_Loan_App.Domain.Entities;

namespace Mini_Loan_App.Infrastructure.Repositories;

public interface ILoanRepository
{
    Task<Loan> CreateAsync(Loan loan);
    Task<Loan?> GetByIdAsync(Guid id);
}