using eMuhasebeServer.Application.Features.BankDetails.CreateBankDetail;
using eMuhasebeServer.Application.Features.BankDetails.DeleteBankDetailById;
using eMuhasebeServer.Application.Features.BankDetails.GetAllBankDetails;
using eMuhasebeServer.Application.Features.BankDetails.UpdateBankDetail;
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

public sealed class BankDetailsController : ApiController
{
    public BankDetailsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllBankDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBankDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBankDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteBankDetailByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
