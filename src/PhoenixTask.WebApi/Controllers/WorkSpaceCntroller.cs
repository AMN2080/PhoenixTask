using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Contracts.WorkSpaces;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

public class WorkSpaceCntroller(IMediator mediator):ApiController(mediator)
{
    [HttpGet(ApiRoutes.WorkSpace.GetAllWorkSpaces)]
    [ProducesResponseType(typeof(List<WorkSpaceResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Get()
    {
        throw new NotImplementedException();
    }
    [HttpGet(ApiRoutes.WorkSpace.GetById)]
    [ProducesResponseType(typeof(WorkSpaceResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Get(Guid workspaceId)
    {
        throw new NotImplementedException();
    }
    [HttpPost(ApiRoutes.WorkSpace.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Create(WorkSpace workSpace)
    {
        throw new NotImplementedException();
    }
    [HttpPut(ApiRoutes.WorkSpace.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Update(Guid workspaceId,WorkSpace workSpace)
    {
        throw new NotImplementedException();
    }
    [HttpDelete(ApiRoutes.WorkSpace.Remove)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> Delete(Guid workspaceId)
    {
        throw new NotImplementedException();
    }
}
