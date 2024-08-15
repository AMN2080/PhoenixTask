using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Contracts.WorkSpaces;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Application.WorkSpaces.GetAllWorkSpaces;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Application.WorkSpaces.GetWorkSpaceById;
using PhoenixTask.Application.WorkSpaces.CreateWorkSpace;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Application.WorkSpaces.UpdateWorkSpace;
using PhoenixTask.Application.WorkSpaces.DeleteWorkSpace;

namespace PhoenixTask.WebApi.Controllers;

public class WorkSpaceController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet(ApiRoutes.WorkSpace.GetAllWorkSpaces)]
    [ProducesResponseType(typeof(List<WorkSpaceResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
        => await Maybe<GetAllWorkspacesQuery>.From(new GetAllWorkspacesQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.WorkSpace.GetById)]
    [ProducesResponseType(typeof(WorkSpaceResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(Guid workspaceId)
        => await Maybe<WorkspaceByIdQuery>.From(new WorkspaceByIdQuery(workspaceId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);


    [HttpPost(ApiRoutes.WorkSpace.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Create([FromBody]WorkSpace workSpace)
        => Result.Success(new CreateWorkSpaceCommand(workSpace.Name, workSpace.Color))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, NotFound);

    [HttpPut(ApiRoutes.WorkSpace.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid workspaceId,[FromBody] WorkSpaceResult workSpace) 
        => await Result.Create(workSpace, DomainErrors.General.UnProcessableRequest)
            .Ensure(request => request.Id == workspaceId, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateWorkspaceCommand(workspaceId, request.Name, request.Color))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpDelete(ApiRoutes.WorkSpace.Remove)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid workspaceId) 
        => await Result.Success(new DeleteWorkspaceCommand(workspaceId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}
