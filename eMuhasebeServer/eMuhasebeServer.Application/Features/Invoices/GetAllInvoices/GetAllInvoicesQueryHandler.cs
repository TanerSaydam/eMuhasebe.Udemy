using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Invoices.GetAllInvoices;

internal sealed class GetAllInvoicesQueryHandler(
    IInvoiceRepository invoiceRepository,
    ICacheService cacheService) : IRequestHandler<GetAllInvoicesQuery, Result<List<Invoice>>>
{
    public async Task<Result<List<Invoice>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        List<Invoice>? invoices;
        string key = "";

        if(request.Type == 1)
        {
            key = "purchaseInvoices";           
        }
        else
        {
            key = "sellingInvoices";           
        }

        invoices = cacheService.Get<List<Invoice>>(key);

        if (invoices is null)
        {
            invoices = await invoiceRepository.Where(p => p.Type.Value == request.Type).OrderBy(p => p.Date).ToListAsync(cancellationToken);

            cacheService.Set(key, invoices);
        }

        return invoices;
    }
}
