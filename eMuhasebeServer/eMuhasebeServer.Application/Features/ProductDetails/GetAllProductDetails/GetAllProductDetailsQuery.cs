using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ProductDetails.GetAllProductDetails;
public sealed record GetAllProductDetailsQuery(
    Guid ProductId) : IRequest<Result<Product>>;
