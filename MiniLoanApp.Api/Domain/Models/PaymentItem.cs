namespace Mini_Loan_App.Domain.Models;

public record PaymentItem(
    int Period,
    decimal PrincipalPart,
    decimal InterestPart,
    decimal Total,
    DateTimeOffset DueDate
);
