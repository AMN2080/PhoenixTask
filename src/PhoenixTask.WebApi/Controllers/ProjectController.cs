using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Contracts.Projects;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

public class ProjectController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet(ApiRoutes.Projects.GetWorkSpaceProjects)]
    [ProducesResponseType(typeof(List<ProjectResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public IActionResult Get(Guid workspaceId) => throw new NotImplementedException();

    [HttpGet(ApiRoutes.Projects.GetById)]
    [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Get(Guid workspaceId, Guid projectId) => throw new NotImplementedException();

    [HttpPost(ApiRoutes.Projects.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Create(Guid workspaceId, [FromBody] Project project) => throw new NotImplementedException();

    [HttpPut(ApiRoutes.Projects.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Update(Guid workspaceId, [FromBody] Project project) => throw new NotImplementedException();

    [HttpDelete(ApiRoutes.Projects.Remove)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(Guid workspaceId, Guid projectId) => throw new NotImplementedException();
}
