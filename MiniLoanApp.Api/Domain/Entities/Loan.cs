using Mini_Loan_App.Domain.Enums;

namespace Mini_Loan_App.Domain.Entities;

public class Loan
{
    public Guid Id { get; set; }
    public string ApplicantName { get; set; } = string.Empty;
    public decimal Principal { get; set; }
    public decimal AnnualRate { get; set; }
    public int TermMonths { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.Pending;
}