using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Customers.UpdateCustomer;
public sealed record UpdateCustomerCommand(
    Guid Id,
    string Name,
    int TypeValue,
    string City,
    string Town,
    string FullAddress,
    string TaxDepartment,
    string TaxNumber): IRequest<Result<string>>;
