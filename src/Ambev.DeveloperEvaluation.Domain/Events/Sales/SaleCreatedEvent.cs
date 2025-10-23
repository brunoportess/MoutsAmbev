using Ambev.DeveloperEvaluation.Domain.Sales;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales;

public record SaleCreatedEvent(Sale Sale) : INotification;
