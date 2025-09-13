using Mini_Loan_App.Controllers.Dtos;
using Mini_Loan_App.Domain;
using Mini_Loan_App.Infrastructure.Repositories;
using Mini_Loan_App.Services.Interfaces;

namespace Mini_Loan_App.Services;

public class LoanService: ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IApprovalPolicy _approvalPolicy;

    public LoanService(ILoanRepository loanRepository, IApprovalPolicy approvalPolicy)
    {
        _loanRepository = loanRepository;
        _approvalPolicy = approvalPolicy;
    }

    public async Task<Loan> CreateAsync(CreateLoanRequest request)
    {
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            ApplicantName = request.ApplicantName,
            Principal = request.Principal,
            AnnualRate = request.AnnualRate,
            TermMonths = request.TermMonths,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = _approvalPolicy.Decide(request.Principal, request.AnnualRate, request.TermMonths, request.ApplicantScore ?? 0.7m)
        };

        await _loanRepository.CreateAsync(loan);
        return loan;
    }
}