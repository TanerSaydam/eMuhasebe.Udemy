using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Invoices.GetAllInvoices;
public sealed record GetAllInvoicesQuery() : IRequest<Result<List<Invoice>>>;
