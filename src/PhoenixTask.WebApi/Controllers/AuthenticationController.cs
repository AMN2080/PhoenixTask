using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Authentication.Login;
using PhoenixTask.Application.Users.CreateUser;
using PhoenixTask.Contracts.Authentication;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;
[AllowAnonymous]
public class AuthenticationController(IMediator mediator) : ApiController(mediator)
{
    [HttpPost(ApiRoutes.Authentication.Login)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest) =>
     await Result.Create(loginRequest, DomainErrors.General.UnProcessableRequest)
        .Map(request => new LoginWithUserNameCommand(request.Username, request.Password))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.Authentication.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserRequest)
    => await Result.Create(createUserRequest, DomainErrors.General.UnProcessableRequest)
    .Map(request => new CreateUserCommand(request.Username, request.Email, request.Password))
    .Bind(command => Mediator.Send(command))
    .Match(Ok, BadRequest);
}