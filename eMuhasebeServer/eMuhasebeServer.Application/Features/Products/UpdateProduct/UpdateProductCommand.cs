using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Products.UpdateProduct;
public sealed record UpdateProductCommand(
    Guid Id,
    string Name) : IRequest<Result<string>>;
