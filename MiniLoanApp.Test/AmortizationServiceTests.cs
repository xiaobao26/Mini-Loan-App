using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Mini_Loan_App.Services;
using Mini_Loan_App.Services.Interfaces;

namespace MiniLoanApp.Test;

public class AmortizationServiceTests
{
    private readonly IAmortizationService _amortizationService;

    public AmortizationServiceTests()
    {
        _amortizationService = new AmortizationService();
    }

    [Fact]
    public void GenerateCorrectNumberOfPayments()
    {
        var schedule = _amortizationService.GenerateSchedule(DateTime.Today, 12_000m, 0.12m, 12);
        schedule.Should().HaveCount(12);
    }

    [Fact]
    public void ZeroInterestShouldSplitPrincipalEqually()
    {
        var schedule = _amortizationService.GenerateSchedule(DateTime.Today, 12_000m, 0m, 12);
        schedule.All(x => x.InterestPart == 0).Should().BeTrue();
        schedule.All(x => x.PrincipalPart == 1_000m).Should().BeTrue();
    }

    [Fact]
    public void FirstMonthPaymentCheck()
    {
        var schedule = _amortizationService.GenerateSchedule(DateTime.Today, 12_000m, 0.12m, 12);
        var firstPayment = schedule[0];
        firstPayment.InterestPart.Should().BeApproximately(120m, 0.01m);
        firstPayment.PrincipalPart.Should().BeApproximately(946.19m, 0.01m);
        (firstPayment.InterestPart + firstPayment.PrincipalPart).Should().BeApproximately(1066.19m, 0.01m);
    }

    [Fact]
    public void LastMonthPaymentShouldClearRemainingBalance()
    {
        var schedule = _amortizationService.GenerateSchedule(DateTime.Today, 12_000m, 0.12m, 12);
        var lastPayment = schedule[^1];

        var remaining = 12_000m - schedule.Sum(x => x.PrincipalPart);
        remaining.Should().Be(0);

        (lastPayment.PrincipalPart + lastPayment.InterestPart).Should().BeApproximately(lastPayment.Total, 0.01m);
    }
    
    [Fact]
    public void RemainingPrincipalShouldBeZero()
    {
        var schedule = _amortizationService.GenerateSchedule(DateTime.Today, 12_000m, 0.12m, 12);
        var totalPrincipal = schedule.Sum(x => x.PrincipalPart);
        totalPrincipal.Should().BeApproximately(12_000m, 0.01m);
    }

    [Fact]
    public void SingleMonthLoanShouldRepayFullPrincipal()
    {
        var schedule = _amortizationService.GenerateSchedule(DateTime.Today, 5_000m, 0.1m, 1);
        schedule.Should().HaveCount(1);

        schedule[0].PrincipalPart.Should().Be(5_000m);
        schedule[0].InterestPart.Should().BeApproximately(41.67m, 0.01m);
    }

    [Fact]
    public void TotalPaymentShouldEqualPrincipalPlusInterest()
    {
        var schedule = _amortizationService.GenerateSchedule(DateTime.Today, 12_000m, 0.12m, 12);
        var totalPayments = schedule.Sum(x => x.Total);
        var totalPrincipal = schedule.Sum(x => x.PrincipalPart);
        var totalInterest = schedule.Sum(x => x.InterestPart);
        totalPayments.Should().Be(totalPrincipal + totalInterest);
    }
}