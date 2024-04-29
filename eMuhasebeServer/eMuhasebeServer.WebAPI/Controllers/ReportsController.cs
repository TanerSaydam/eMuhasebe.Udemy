using eMuhasebeServer.Application.Features.Reports.ProductProfitabilityReports;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class ReportsController : ApiController
{
    public ReportsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> ProductProfitabilityReports(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ProductProfitabilityReportsQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
