using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Authentication.Login;
using PhoenixTask.Contracts.Authentication;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers
{
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
    }
}
