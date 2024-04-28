using eMuhasebeServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Invoices.CreateInvoice;
public sealed record CreateInvoiceCommand(
    int TypeValue,
    DateOnly Date,
    string InvoiceNumber,
    Guid CustomerId,
    List<InvoiceDetailDto> InvoiceDetails
    ) : IRequest<Result<string>>;
