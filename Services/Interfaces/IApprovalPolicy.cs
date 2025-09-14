using Mini_Loan_App.Domain.Enums;

namespace Mini_Loan_App.Services.Interfaces;

public interface IApprovalPolicy
{
    LoanStatus Decide(decimal principal, decimal annualRate, int termMonths, decimal applicantScore);
}