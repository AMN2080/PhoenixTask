using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Application.WorkSpaces.AddUserToWorkSpace;

internal sealed class CreateWorkSpaceMemberCommandHandler(
    IWorkSpaceRepository workSpaceRepository,
    IUserRepository userRepository,
    IWorkSpaceMemberRepository workSpaceMemberRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateWorkSpaceMemberCommand, Result>
{
    private readonly IWorkSpaceMemberRepository _workSpaceMemberRepository = workSpaceMemberRepository;
    private readonly IWorkSpaceRepository _workSpaceRepository = workSpaceRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(CreateWorkSpaceMemberCommand request, CancellationToken cancellationToken)
    {
        var maybeWorkSpace = await _workSpaceRepository.GetByIdAsync(request.WorkSpaceId);

        if (maybeWorkSpace.HasNoValue)
        {
            return Result.Failure(DomainErrors.WorkSpace.NotFound);
        }

        var maybeRole = Role.FromValue(request.RoleId);

        if (maybeRole.HasNoValue)
        {
            return Result.Failure(DomainErrors.Role.NotFound);
        }

        var maybeUser = await _userRepository.GetByIdAsync(request.UserId);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var userAlreadyHasRole = await _workSpaceMemberRepository.UserHasRoleAsync(maybeUser.Value, maybeWorkSpace.Value, maybeRole.Value);

        if (userAlreadyHasRole)
        {
            return Result.Success();
        }

        var member = WorkSpaceMember.Create(maybeWorkSpace.Value, maybeUser.Value, maybeRole.Value);
        
        _workSpaceMemberRepository.Insert(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
