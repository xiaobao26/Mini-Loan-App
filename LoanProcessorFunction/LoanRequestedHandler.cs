using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LoanProcessorFunction;

public class LoanRequestedHandler
{
    private readonly ILogger<LoanRequestedHandler> _logger;

    public LoanRequestedHandler(ILogger<LoanRequestedHandler> logger)
    {
        _logger = logger;
    }

    [Function("LoanRequested")]
    public void Run([ServiceBusTrigger("loanapplicationtopic", "LoanProcessorSub", Connection = "ServiceBusConnection")] string loanId)
    {

        // Complete the message
        _logger.LogInformation($"Received LoanRequested event for LoanId={loanId}");
    }
}