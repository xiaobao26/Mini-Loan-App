using Mini_Loan_App.Domain.Models;
using Mini_Loan_App.Services.Interfaces;

namespace Mini_Loan_App.Services;

public class AmortizationService: IAmortizationService
{
    public IReadOnlyList<PaymentItem> GenerateSchedule(DateTime firstDueDate, decimal principal, decimal annualRate, int termMonths)
    {
        var monthlyRate = annualRate / 12m;
        var monthlyPayment = CalculateMonthlyPayment(principal, monthlyRate, termMonths);
        
        var schedule = new List<PaymentItem>(termMonths);
        var remaining = principal;
        var dueDate = firstDueDate;

        for (var i = 1; i <= termMonths; i++)
        {
            var interest = Math.Round(remaining * monthlyRate, 2, MidpointRounding.AwayFromZero);
            var principalPart = Math.Round(monthlyPayment - interest, 2, MidpointRounding.AwayFromZero);

            // Ensure the last installment clears the balance (rounding fix)
            if (i == termMonths)
            {
                principalPart = remaining;
                monthlyPayment = principalPart + interest;
            }

            remaining = Math.Round(remaining - principalPart, 2, MidpointRounding.AwayFromZero);

            schedule.Add(new PaymentItem(i, principalPart, interest, principalPart + interest, dueDate));
            dueDate = dueDate.AddMonths(1);
        }

        return schedule;
    }

    /// <summary>
    /// Calculate the fixed monthly payment using the EMI formula.
    /// </summary>
    private static decimal CalculateMonthlyPayment(decimal principal, decimal monthlyRate, int termMonths)
    {
        if (monthlyRate == 0)
            return Math.Round(principal / termMonths, 2, MidpointRounding.AwayFromZero);

        var pow = (decimal)Math.Pow((double)(1 + monthlyRate), termMonths);
        return Math.Round(principal * monthlyRate * pow / (pow - 1), 2, MidpointRounding.AwayFromZero);
    }
}