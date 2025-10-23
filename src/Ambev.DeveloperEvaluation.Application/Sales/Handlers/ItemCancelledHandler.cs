using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class ItemCancelledHandler : INotificationHandler<ItemCancelledEvent>
{
    private readonly ILogger<ItemCancelledHandler> _logger;

    public ItemCancelledHandler(ILogger<ItemCancelledHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handled ItemCancelledEvent: {SaleId}", notification.Sale.Id);
        return Task.CompletedTask;
    }
}
