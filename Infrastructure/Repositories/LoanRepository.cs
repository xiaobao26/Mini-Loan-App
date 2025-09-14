
using Mini_Loan_App.Domain.Entities;

namespace Mini_Loan_App.Infrastructure.Repositories;

public class LoanRepository: ILoanRepository
{
    private readonly LoanDbContext _dbContext;

    public LoanRepository(LoanDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Loan> CreateAsync(Loan loan)
    {
        await _dbContext.AddAsync(loan);
        await _dbContext.SaveChangesAsync();

        return loan;
    }

    public async Task<Loan?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Loans.FindAsync(id);
    }
}