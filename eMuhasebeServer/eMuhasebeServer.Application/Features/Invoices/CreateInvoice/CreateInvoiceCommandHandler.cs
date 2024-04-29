using AutoMapper;
using eMuhasebeServer.Application.Hubs;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Invoices.CreateInvoice;

internal sealed class CreateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IProductRepository productRepository,
    IProductDetailRepository productDetailRepository,
    ICustomerRepository customerRepository,
    ICustomerDetailRepository customerDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService,
    IMapper mapper,
    IHubContext<ReportHub> hubContext) : IRequestHandler<CreateInvoiceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        #region Fatura ve detay
        Invoice invoice = mapper.Map<Invoice>(request);

        await invoiceRepository.AddAsync(invoice, cancellationToken);
        #endregion

        #region Customer
        Customer? customer = await customerRepository.GetByExpressionAsync(p => p.Id == request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result<string>.Failure("Müşteri bulunamadı");
        }

        customer.DepositAmount += request.TypeValue == 2 ? invoice.Amount : 0;
        customer.WithdrawalAmount += request.TypeValue == 1 ? invoice.Amount : 0;

        customerRepository.Update(customer);

        CustomerDetail customerDetail = new()
        {
            CustomerId = customer.Id,
            Date = request.Date,
            DepositAmount = request.TypeValue == 2 ? invoice.Amount : 0,
            WithdrawalAmount = request.TypeValue == 1 ? invoice.Amount : 0,
            Description = invoice.InvoiceNumber + " Numaralı " + invoice.Type.Name,
            Type = request.TypeValue == 1 ? CustomerDetailTypeEnum.PurchaseInvoice : CustomerDetailTypeEnum.SellingInvoice,
            InvoiceId = invoice.Id
        };

        await customerDetailRepository.AddAsync(customerDetail, cancellationToken);
        #endregion

        #region Product        
        foreach (var item in request.Details)
        {
            Product product = await productRepository.GetByExpressionAsync(p => p.Id == item.ProductId, cancellationToken);

            product.Deposit += request.TypeValue == 1 ? item.Quantity : 0;
            product.Withdrawal += request.TypeValue == 2 ? item.Quantity : 0;

            productRepository.Update(product);

            ProductDetail productDetail = new()
            {
                ProductId = product.Id,
                Date = request.Date,
                Description = invoice.InvoiceNumber + " Numaralı " + invoice.Type.Name,
                Deposit = request.TypeValue == 1 ? item.Quantity : 0,
                Withdrawal = request.TypeValue == 2 ? item.Quantity : 0,
                InvoiceId = invoice.Id,
                Price = item.Price
            };

            await productDetailRepository.AddAsync(productDetail, cancellationToken);
        }        
        #endregion

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("invoices");
        cacheService.Remove("customers");
        cacheService.Remove("products");


        if(invoice.Type == InvoiceTypeEnum.Selling)
        {
            await hubContext.Clients.All.SendAsync("PurchaseRepors", new { Date = invoice.Date, Amount = invoice.Amount });
        }

        return invoice.Type.Name + " kaydı başarıyla tamamlandı";
    }
}
