using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Projects.CreateProject;
using PhoenixTask.Application.Projects.DeleteProject;
using PhoenixTask.Application.Projects.GetProjectById;
using PhoenixTask.Application.Projects.GetProjectByWorkSpace;
using PhoenixTask.Application.Projects.UpdateProject;
using PhoenixTask.Contracts.Projects;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

public class ProjectController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet(ApiRoutes.Projects.GetWorkSpaceProjects)]
    [ProducesResponseType(typeof(List<ProjectResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid workspaceId)
        => Ok(await Mediator.Send(new GetProjectByWorkSpaceQuery(workspaceId)));

    [HttpGet(ApiRoutes.Projects.GetById)]
    [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid projectId)
        => await Result.Success(new GetProjectByIdQuery(projectId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Projects.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid workspaceId, [FromBody] Project project)
        => await Result.Success(new CreateProjectCommand(project.Name, workspaceId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpPut(ApiRoutes.Projects.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] ProjectResult project)
        => await Result.Success(new UpdateProjectCommand(project.Id, project.Name))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpDelete(ApiRoutes.Projects.Remove)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid projectId)
        => await Result.Success(new DeleteProjectCommand(projectId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}