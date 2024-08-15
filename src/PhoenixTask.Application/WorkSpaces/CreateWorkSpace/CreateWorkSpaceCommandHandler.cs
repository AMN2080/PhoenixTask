using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.CreateWorkSpace;

internal sealed class CreateWorkSpaceCommandHandler
    (IUserIdentifierProvider userIdentifierProvider,
    IUserRepository userRepository,
    IWorkSpaceRepository workSpaceRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateWorkSpaceCommand, Result<string>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<string>> Handle(CreateWorkSpaceCommand request, CancellationToken cancellationToken)
    {
        var userId=_userIdentifierProvider.UserId;
        var maybeUser = await _userRepository.GetByIdAsync(userId);
        if (maybeUser.HasNoValue)
        {
            return Result.Failure<string>(DomainErrors.User.NotFound);
        }

        var nameResult = Name.Create(request.Name);
        // color validated by fluent validation

        if (nameResult.IsFailure)
        {
            return Result.Failure<string>(nameResult.Error);
        }

        var workSpace = WorkSpace.Create(maybeUser.Value, nameResult.Value, request.Color);

        _workSpaceRepository.Insert(workSpace);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return workSpace.Id.ToString();
    }
}
