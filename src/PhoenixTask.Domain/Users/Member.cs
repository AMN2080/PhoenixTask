using PhoenixTask.Domain.Abstractions;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Domain.Users;

public abstract class Member : AggregateRoot , IAuditableEntity , ISoftDeletableEntity
{
    protected Member(User user,ICollection<Role> roles)
        :base(Guid.NewGuid())
    {

        User = user;
        UserId = user.Id;
        Roles = roles;
    }
    /// <summary>
    /// efcore
    /// </summary>
    #pragma warning disable
    public Member()
    {
        
    }
#pragma warning restore
    public Guid UserId { get; private set; }
    
    public User User { get; private set; }

    public ICollection<Role> Roles { get; protected set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public void Update(params Role[] roles)
    {
        Roles = roles;
    }
}
