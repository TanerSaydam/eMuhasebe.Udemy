using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CustomerDetails.GetAllCustomerDetails;
public sealed record GetAllCustomerDetailsQuery(
    Guid CustomerId) : IRequest<Result<Customer>>;
