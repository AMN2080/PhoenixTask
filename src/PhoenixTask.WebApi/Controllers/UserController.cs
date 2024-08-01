using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Authentication.Login;
using PhoenixTask.Application.Users.CreateUser;
using PhoenixTask.Contracts.Authentication;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ApiController
{
    protected UserController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost(ApiRoutes.Users.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateUserRequest createUserRequest) 
        => await Result.Create(createUserRequest, DomainErrors.General.UnProcessableRequest)
        .Map(request => new CreateUserCommand(request.Username, request.Email, request.Password))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}
