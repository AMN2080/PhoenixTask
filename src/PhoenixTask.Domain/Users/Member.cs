using PhoenixTask.Domain.Abstractions;
using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Domain.Users;

public abstract class Member : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    protected Member(User user, Role role, MemberType memberType)
        : base(Guid.NewGuid())
    {
        Ensure.NotNull(user, "The user is requierd.", nameof(user));
        Ensure.NotNull(role, "The role is requierd.", nameof(role));

        UserId = user.Id;
        RoleValue = role.Value;
        MemberType = memberType;
    }
    /// <summary>
    /// efcore
    /// </summary>
#pragma warning disable
    protected Member()
    {

    }
#pragma warning restore
    public Guid UserId { get; private set; }

    public int RoleValue { get; protected set; }
    public MemberType MemberType { get;private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public void Update(Role role)
    {
        Ensure.NotNull(role, "The role is requierd.", nameof(role));

        RoleValue = role.Value;
    }
}
