using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class SaleModifiedHandler : INotificationHandler<SaleModifiedEvent>
{
    private readonly ILogger<SaleModifiedHandler> _logger;

    public SaleModifiedHandler(ILogger<SaleModifiedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handled SaleModifiedEvent: {SaleId}", notification.Sale.Id);
        return Task.CompletedTask;
    }
}
