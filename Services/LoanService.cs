using Mini_Loan_App.Controllers.Dtos;
using Mini_Loan_App.Domain.Entities;
using Mini_Loan_App.Domain.Models;
using Mini_Loan_App.Infrastructure.Repositories;
using Mini_Loan_App.Services.Interfaces;

namespace Mini_Loan_App.Services;

public class LoanService: ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IApprovalPolicy _approvalPolicy;
    private readonly IAmortizationService _amortizationService;

    public LoanService(ILoanRepository loanRepository, IApprovalPolicy approvalPolicy, IAmortizationService amortizationService)
    {
        _loanRepository = loanRepository;
        _approvalPolicy = approvalPolicy;
        _amortizationService = amortizationService;
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

    public async Task<Loan?> GetByIdAsync(Guid id)
    {
        return await _loanRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<PaymentItem>> GetScheduleAsync(Guid loanId, DateTime firstDueDate)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId)
                   ?? throw new KeyNotFoundException("Loan not found");

        return _amortizationService.GenerateSchedule(firstDueDate, loan.Principal, loan.AnnualRate, loan.TermMonths);
    }
}