using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Authentication.ChangePassword;
using PhoenixTask.Application.Authentication.ForgetPassword;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

public class UserController(IMediator mediator) : ApiController(mediator)
{
    [HttpPost(ApiRoutes.Users.ChangePassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromRoute]Guid userId,[FromBody] ChangePasswordRequest changePasswordRequest) =>
     await Result.Create(changePasswordRequest, DomainErrors.General.UnProcessableRequest)
        .Ensure(request => request.UserId == userId, DomainErrors.General.UnProcessableRequest)
        .Map(request => new ChangePasswordCommand(request.UserId, request.Password))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.Users.ForgetPassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgetPassword([FromBody]ForgetPasswordRequest forgetPasswordRequest) =>
     await Result.Create(forgetPasswordRequest, DomainErrors.General.UnProcessableRequest)
        .Map(request => new ForgetPasswordCommand(request.Email))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.Users.ResetPassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest resetPasswordRequest) =>
     await Result.Create(resetPasswordRequest, DomainErrors.General.UnProcessableRequest)
        .Map(request => new ResetPasswordCommand(request.Email,request.Token, request.Password))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}
