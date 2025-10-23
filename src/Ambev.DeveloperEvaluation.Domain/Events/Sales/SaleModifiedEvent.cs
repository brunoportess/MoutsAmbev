using Ambev.DeveloperEvaluation.Domain.Sales;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales;

public record SaleModifiedEvent(Sale Sale) : INotification;
