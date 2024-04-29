using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.PurchaseReports;

internal sealed class PurchaseReportsQueryHandler(
    IInvoiceRepository invoiceRepository) : IRequestHandler<PurchaseReportsQuery, Result<object>>
{
    public async Task<Result<object>> Handle(PurchaseReportsQuery request, CancellationToken cancellationToken)
    {
        List<Invoice> invoices = await invoiceRepository.Where(p => p.Type == 2).OrderBy(p => p.Date).ToListAsync(cancellationToken);

        var response = new
        {
            Dates = invoices.GroupBy(group => group.Date).Select(s => s.Key).ToList(),
            Amounts = invoices.GroupBy(group => group.Date).Select(s => s.Sum(s => s.Amount)).ToList()
        };

        return response;
    }
}
