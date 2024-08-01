using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Authentication.ChangePassword;
using PhoenixTask.Application.Authentication.ForgetPassword;
using PhoenixTask.Application.Authentication.Login;
using PhoenixTask.Contracts.Authentication;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ApiController
{
    protected AuthenticationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost(ApiRoutes.Authentication.Login)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginRequest loginRequest) =>
     await Result.Create(loginRequest, DomainErrors.General.UnProcessableRequest)
        .Map(request => new LoginWithUserNameCommand(request.Username, request.Password))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.Users.ChangePassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest) =>
     await Result.Create(changePasswordRequest, DomainErrors.General.UnProcessableRequest)
        .Ensure(request => request.UserId == userId, DomainErrors.General.UnProcessableRequest)
        .Map(request => new ChangePasswordCommand(request.UserId, request.OldPassword, request.NewPassword))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.Authentication.ForgetPassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest) =>
     await Result.Create(forgetPasswordRequest, DomainErrors.General.UnProcessableRequest)
        .Map(request => new ForgetPasswordCommand(request.Email))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.Authentication.ResetPassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest) =>
     await Result.Create(resetPasswordRequest, DomainErrors.General.UnProcessableRequest)
        .Map(request => new ResetPasswordCommand(request.Token, request.Password))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}