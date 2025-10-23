using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class SaleCreatedHandler : INotificationHandler<SaleCreatedEvent>
{
    private readonly ILogger<SaleCreatedHandler> _logger;

    public SaleCreatedHandler(ILogger<SaleCreatedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handled SaleCreatedEvent: {SaleId}", notification.Sale.Id);
        return Task.CompletedTask;
    }
}
