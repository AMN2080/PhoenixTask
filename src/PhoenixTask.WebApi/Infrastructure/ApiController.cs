using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.WebApi.Contract;

namespace PhoenixTask.WebApi.Infrastructure;

[Authorize]
[Route("api")]
public class ApiController : ControllerBase
{
    protected ApiController(IMediator mediator) => Mediator = mediator;

    protected IMediator Mediator { get; }
    protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));

    protected new IActionResult Ok(object value) => base.Ok(value);

    protected new IActionResult NotFound() => base.NotFound();
}
