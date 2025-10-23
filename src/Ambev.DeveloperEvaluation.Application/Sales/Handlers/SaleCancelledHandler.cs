using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class SaleCancelledHandler : INotificationHandler<SaleCancelledEvent>
{
    private readonly ILogger<SaleCancelledHandler> _logger;

    public SaleCancelledHandler(ILogger<SaleCancelledHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handled SaleCancelledEvent: {SaleId}", notification.Sale.Id);
        return Task.CompletedTask;
    }
}
