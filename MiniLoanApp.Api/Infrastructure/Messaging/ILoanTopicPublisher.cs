namespace Mini_Loan_App.Infrastructure.Messaging;

public interface ILoanTopicPublisher
{
    Task PublishLoanRequestedAsync(Guid loanId);
}