using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.DeleteBankById;
public sealed record DeleteBankByIdCommand(
    Guid Id): IRequest<Result<string>>;
