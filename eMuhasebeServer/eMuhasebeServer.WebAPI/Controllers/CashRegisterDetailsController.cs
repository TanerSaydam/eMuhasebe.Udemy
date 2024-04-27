using eMuhasebeServer.Application.Features.CashRegisterDetails.CreateCashRegisterDetail;
using eMuhasebeServer.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById;
using eMuhasebeServer.Application.Features.CashRegisterDetails.GetAllCashRegisterDetails;
using eMuhasebeServer.Application.Features.CashRegisterDetails.UpdateCashRegisterDetail;
using eMuhasebeServer.Application.Features.CashRegisters.CreateCashRegister;
using eMuhasebeServer.Application.Features.CashRegisters.DeleteCashRegisterById;
using eMuhasebeServer.Application.Features.CashRegisters.GetAllCashRegisters;
using eMuhasebeServer.Application.Features.CashRegisters.UpdateCashRegister;
using eMuhasebeServer.Application.Features.Users.CreateUser;
using eMuhasebeServer.Application.Features.Users.DeleteUserById;
using eMuhasebeServer.Application.Features.Users.GetAllUsers;
using eMuhasebeServer.Application.Features.Users.UpdateUser;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class CashRegisterDetailsController : ApiController
{
    public CashRegisterDetailsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCashRegisterDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCashRegisterDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateCashRegisterDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteCashRegisterDetailByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
