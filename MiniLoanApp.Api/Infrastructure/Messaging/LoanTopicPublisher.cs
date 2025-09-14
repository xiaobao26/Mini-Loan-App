using Azure.Messaging.ServiceBus;

namespace Mini_Loan_App.Infrastructure.Messaging;

public class LoanTopicPublisher:ILoanTopicPublisher, IAsyncDisposable
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;

    public LoanTopicPublisher(string connectionString, string topic = "loanapplicationtopic")
    {
        _client = new ServiceBusClient(connectionString);
        _sender = _client.CreateSender(topic);
    }

    public async Task PublishLoanRequestedAsync(Guid loanId)
    {
        var message = new ServiceBusMessage(loanId.ToString())
        {
            Subject = "LoanRequested",
            ContentType = "text/plain"
        };

        await _sender.SendMessageAsync(message);
    }
    
    // Clean Up
    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
        await _client.DisposeAsync();
    }
}