using Mini_Loan_App.Domain.Enums;

namespace Mini_Loan_App.Controllers.Dtos;

public sealed record CreateLoanRequest(
    string ApplicantName,
    decimal Principal,
    decimal AnnualRate,
    int TermMonths,
    decimal? ApplicantScore
);

public sealed record LoanResponse(
    Guid Id,
    string ApplicantName,
    decimal Principal,
    decimal AnnualRate,
    int TermMonths,
    DateTimeOffset CreatedAt,
    LoanStatus? Status
);