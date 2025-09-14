using Mini_Loan_App.Controllers.Dtos;
using Mini_Loan_App.Domain.Entities;
using Mini_Loan_App.Domain.Models;

namespace Mini_Loan_App.Services.Interfaces;

public interface ILoanService
{
    Task<Loan> CreateAsync(CreateLoanRequest request);
    Task<Loan?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<PaymentItem>> GetScheduleAsync(Guid loanId, DateTime firstDueDate);
}