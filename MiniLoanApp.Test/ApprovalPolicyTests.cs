using Mini_Loan_App.Domain.Enums;
using Mini_Loan_App.Services.Interfaces;
using FluentAssertions;
using Mini_Loan_App.Services;

namespace MiniLoanApp.Test;

public class ApprovalPolicyTests
{
    private readonly IApprovalPolicy _approvalPolicy;

    public ApprovalPolicyTests()
    {
        _approvalPolicy = new ApprovalPolicy();
    }

    [Fact]
    public void RejectWhenScoreTooLow()
    {
        var status = _approvalPolicy.Decide(50_000m, 0.1m, 12, 0.1m);
        status.Should().Be(LoanStatus.Rejected);
    }

    [Fact]
    public void ApproveWhenSmallLoanReasonableRate()
    {
        var status = _approvalPolicy.Decide(10_000m, 0.08m, 12, 0.6m);
        status.Should().Be(LoanStatus.Approved);
    }

    [Fact]
    public void PendingWhenHighPrincipal()
    {
        var status = _approvalPolicy.Decide(1_000_000m, 0.20m, 24, 0.6m);
        status.Should().Be(LoanStatus.Pending);
    }

    [Fact]
    public void RejectWhenInvalidPrincipal()
    {
        var status = _approvalPolicy.Decide(0, 0.1m, 12, 0.6m);
        status.Should().Be(LoanStatus.Rejected);
    }

    [Fact]
    public void ApproveWhenAtBoundaryValues()
    {
        var status = _approvalPolicy.Decide(50_000m, 0.20m, 84, 0.5m);
        status.Should().Be(LoanStatus.Approved);
    }
}