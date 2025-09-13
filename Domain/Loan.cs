using Mini_Loan_App.Domain.Enum;

namespace Mini_Loan_App.Domain;

public class Loan
{
    public Guid Id { get; set; }
    public string ApplicantName { get; set; } = string.Empty;
    public decimal  Principal { get; set; }
    public decimal AnnualRate { get; set; }
    public int TermMonths { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.Pending;
}