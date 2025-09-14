using Mini_Loan_App.Domain.Enums;
using Mini_Loan_App.Services.Interfaces;

namespace Mini_Loan_App.Services;

public class ApprovalPolicy: IApprovalPolicy
{
    public LoanStatus Decide(decimal principal, decimal annualRate, int termMonths, decimal applicantScore)
    {
        if (principal <= 0 || annualRate < 0 || termMonths <= 0)
        {
            return LoanStatus.Rejected;
        }
        // Reject if applicant's score is too low (high risk).
        if (applicantScore < 0.5m)
        {
            return LoanStatus.Rejected;
        }
        
        // Approve automatically if loan is small, rate is reasonable, and term is short.
        if (principal <= 50_000m && annualRate <= 0.20m && termMonths <= 84)
        {
            return LoanStatus.Approved;
        }
        
        // Otherwise, leave the loan Pending (requires manual review).
        return LoanStatus.Pending;
    }
}