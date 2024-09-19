using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Projects.CreateProject;
using PhoenixTask.Application.Projects.CreateProjectMember;
using PhoenixTask.Application.Projects.DeleteProject;
using PhoenixTask.Application.Projects.DeleteProjectMember;
using PhoenixTask.Application.Projects.GetProjectById;
using PhoenixTask.Application.Projects.GetProjectByWorkSpace;
using PhoenixTask.Application.Projects.GetProjectMembers;
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


    [HttpPost(ApiRoutes.Projects.InviteMember)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Grant(Guid projectId, Guid userId, [FromQuery] int role)
        => await Result.Success(new CreateProjectMemberCommand(projectId, userId, role))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpDelete(ApiRoutes.Projects.RemoveMember)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Revoke(Guid projectId, Guid userId)
        => await Result.Success(new DeleteProjectMemberCommand(projectId, userId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);


    [HttpGet(ApiRoutes.Projects.Members)]
    [ProducesResponseType(typeof(List<ProjectMember>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Members(Guid projectId)
        => await Maybe<GetProjectMemberQuery>.From(new GetProjectMemberQuery(projectId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}