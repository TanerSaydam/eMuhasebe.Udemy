using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Products.GetAllProducts;
public sealed record GetAllProductsQuery() : IRequest<Result<List<Product>>>;
