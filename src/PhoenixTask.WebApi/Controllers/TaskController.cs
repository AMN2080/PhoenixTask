using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Tasks.CreateTask;
using PhoenixTask.Application.Tasks.DeleteTask;
using PhoenixTask.Application.Tasks.GetTasks.GetTasksByBoard;
using PhoenixTask.Application.Tasks.UpdateTask;
using PhoenixTask.Contracts.Tasks;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

public class TaskController(IMediator mediator) : ApiController(mediator)
{
    [HttpPost(ApiRoutes.Tasks.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] TaskModel model)
        => await Result.Success(new CreateTaskCommand(model.Name, model.BoardId, model.Description, model.DeadLine, model.Order, model.Priority))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.Tasks.GetBoardTasks)]
    [ProducesResponseType(typeof(List<TaskResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTasks(Guid boardId)
        => Ok(await Mediator.Send(new GetTasksByBoardQuery(boardId)));

    [HttpPut(ApiRoutes.Tasks.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid taskId, [FromBody] TaskModel model) 
        => await Result.Success(new UpdateTaskCommand(taskId, model.BoardId, model.Name, model.Description, model.DeadLine, model.Order, model.Priority))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpDelete(ApiRoutes.Tasks.Remove)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid taskId)
    => await Result.Success(new DeleteTaskCommand(taskId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest); 
}
