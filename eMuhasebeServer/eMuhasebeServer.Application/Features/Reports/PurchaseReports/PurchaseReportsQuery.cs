using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.PurchaseReports;
public sealed record PurchaseReportsQuery(): IRequest<Result<object>>;
