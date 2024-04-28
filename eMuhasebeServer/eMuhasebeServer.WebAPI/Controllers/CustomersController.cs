using eMuhasebeServer.Application.Features.Banks.CreateBank;
using eMuhasebeServer.Application.Features.Banks.DeleteBankById;
using eMuhasebeServer.Application.Features.Banks.GetAllBanks;
using eMuhasebeServer.Application.Features.Banks.UpdateBank;
using eMuhasebeServer.Application.Features.Customers.CreateCustomer;
using eMuhasebeServer.Application.Features.Customers.DeleteCustomerById;
using eMuhasebeServer.Application.Features.Customers.GetAllCustomers;
using eMuhasebeServer.Application.Features.Customers.UpdateCustomer;
using eMuhasebeServer.Application.Features.Users.CreateUser;
using eMuhasebeServer.Application.Features.Users.DeleteUserById;
using eMuhasebeServer.Application.Features.Users.GetAllUsers;
using eMuhasebeServer.Application.Features.Users.UpdateUser;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class CustomersController : ApiController
{
    public CustomersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
