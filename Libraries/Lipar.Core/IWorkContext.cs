using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Core
{
    public interface IWorkContext
    {
        User CurrentUser { get; set; }
        Position CurrentPosition { get; set; }
        IEnumerable<Position> Positions { get; }
        IEnumerable<Command> Commands { get; set; }
        Center CurrentCenter { get; }
    }
}
