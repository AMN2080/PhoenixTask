using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Tasks.CreateTask;
using PhoenixTask.Contracts.Tasks;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

public class TaskController(IMediator mediator) : ApiController(mediator)
{
    [HttpPost(ApiRoutes.Tasks.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] TaskModel model)
        => await Result.Success(new CreateTaskCommand(model.Name, model.BoardId, model.Description, model.DeadLine, model.Order, model.Priority))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}
