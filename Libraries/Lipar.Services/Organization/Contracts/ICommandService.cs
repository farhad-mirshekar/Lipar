using Lipar.Core;
using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Services.Organization.Contracts
{
    public interface ICommandService
    {
        Command GetById(Guid Id);
        void Add(Command model);
        void Edit(Command model);
        IPagedList<Command> List(CommandListVM listVM);
        bool CheckPermission(string name);
        string GetFormattedBreadCrumb(Command command, string separator = ">>");
        void Delete(Command model);
    }
}
