using AutoMapper;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Auth.SendConfirmEmail;
public sealed record SendConfirmEmailCommand(
    string Email) : IRequest<Result<string>>;
