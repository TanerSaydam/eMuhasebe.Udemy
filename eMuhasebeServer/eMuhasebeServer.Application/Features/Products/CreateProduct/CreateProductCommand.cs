using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Products.CreateProduct;
public sealed record CreateProductCommand(
    string Name) : IRequest<Result<string>>;
