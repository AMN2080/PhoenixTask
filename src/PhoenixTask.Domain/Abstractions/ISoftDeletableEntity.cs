using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixTask.Domain.Abstractions;
public interface ISoftDeletableEntity
{
    DateTime? DeletedOnUtc { get; }
    bool Deleted { get; }
}