using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoenixTask.Application.Boards.CreateBoard;
using PhoenixTask.Application.Boards.DeleteBoard;
using PhoenixTask.Application.Boards.GetBoard;
using PhoenixTask.Application.Boards.GetBoardsByProject;
using PhoenixTask.Application.Boards.UpdateBoard;
using PhoenixTask.Contracts.Boards;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.WebApi.Contract;
using PhoenixTask.WebApi.Infrastructure;

namespace PhoenixTask.WebApi.Controllers;

public class BoardController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet(ApiRoutes.Boards.GetProjectBoards)]
    [ProducesResponseType(typeof(List<BoardResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid projectId)
        => Ok(await Mediator.Send(new GetBoardByProjectQuery(projectId)));

    [HttpGet(ApiRoutes.Boards.GetById)]
    [ProducesResponseType(typeof(BoardResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid boardId)
        => await Maybe<GetBoardQuery>.From(new(boardId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Boards.Create)]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid projectId, [FromBody] Board board)
        => await Result.Success(new CreateBoardCommand(projectId, board.Name, board.Color, board.Order))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpPut(ApiRoutes.Boards.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid boardId, [FromBody] BoardResult board)
        => await Result.Success(new UpdateBoardCommand(boardId, board.Name, board.Order,board.Color))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    [HttpDelete(ApiRoutes.Boards.Remove)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid boardId)
        => await Result.Success(new DeleteBoardCommand(boardId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}
