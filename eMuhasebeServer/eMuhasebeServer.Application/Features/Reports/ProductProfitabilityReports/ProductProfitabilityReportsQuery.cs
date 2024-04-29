using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.ProductProfitabilityReports;
public sealed record ProductProfitabilityReportsQuery() : IRequest<Result<List<ProductProfitabilityReportsQueryResponse>>>;
