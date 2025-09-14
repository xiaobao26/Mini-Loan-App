using Mini_Loan_App.Domain.Models;

namespace Mini_Loan_App.Services.Interfaces;

public interface IAmortizationService
{
    IReadOnlyList<PaymentItem> GenerateSchedule(DateTime firstDueDate, decimal principal, decimal annualRate,
        int termMonths);
}