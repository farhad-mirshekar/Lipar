using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Core.Infrastructure;
using Lipar.Data;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class CommandService : ICommandService
    {
        #region Fields
        private readonly IRepository<Command> _repository;
        #endregion

        #region Ctor
        public CommandService(IRepository<Command> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(Command model)
        {
            _repository.Insert(model);
        }

        public bool CheckPermission(string name)
        {
            var _workContext = EngineContext.Current.Resolve<IWorkContext>();

            if (_workContext.Commands != null)
            {
                var commands = _workContext.Commands.ToList();
                if (commands.Count == 0)
                    return false;

                if (!commands.Any(command => command.SystemName.ToLower().Equals(name.ToLower())))
                    return false;

                return true;
            }

            return false;
        }

        public void Delete(Command model)
        {
            var _workContext = EngineContext.Current.Resolve<IWorkContext>();

            model = GetById(model.Id);
            model.RemoveDate = CommonHelper.GetCurrentDateTime();
            model.RemoverId = _workContext.CurrentUser.Id;

            //update remove date in model current
            Edit(model);

            //check exist child
            var listChilds = List(new CommandListVM { ParentId = model.Id });
            if (listChilds != null && listChilds.Count() > 0)
            {
                foreach (var item in listChilds)
                {
                    item.ParentId = null;
                    Edit(item);
                }
            }
        }

        public void Edit(Command model)
        {
            _repository.Update(model);
        }

        public Command GetById(Guid Id)
        {
            var command = _repository.GetById(Id);

            if (command != null && (command.RemoverId.HasValue && command.RemoverId.Value != Guid.Empty))
                return null;

            return command;
        }

        public string GetFormattedBreadCrumb(Command command, string separator = ">>")
        {
            command = GetById(command.Id);
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            string result = string.Empty;

            var alreadyProcessedCommandId = new List<Guid>();
            while (command != null && command.Id != Guid.Empty && !alreadyProcessedCommandId.Contains(command.Id))
            {
                result = string.IsNullOrEmpty(result) ? command.Name : $"{command.Name} {separator} {result}";

                alreadyProcessedCommandId.Add(command.Id);

                if (command.ParentId.HasValue && command.ParentId.Value != Guid.Empty)
                {
                    command = GetById(command.ParentId.Value);
                }
            }

            return result;
        }

        public IPagedList<Command> List(CommandListVM listVM)
        {
            var query = _repository.TableNoTracking
                                   .Include(c => c.CommandType).AsQueryable();

            query = query.Where(c => c.RemoveDate == null && c.RemoverId == null);

            if (listVM.RoleId.HasValue && listVM.RoleId.Value != Guid.Empty)
                query = query.Where(c => c.RolePermissions.Any(r => r.RoleId == listVM.RoleId));

            if (listVM.ParentId.HasValue && listVM.ParentId.Value != Guid.Empty)
                query = query.Where(c => c.ParentId == listVM.ParentId);

            if (!string.IsNullOrEmpty(listVM.Name))
                query = query.Where(c => c.Name.Contains(listVM.Name.Trim()));

            query.Include(command => command.RolePermissions);

            var models = new PagedList<Command>(query, listVM.PageIndex, listVM.PageSize);

            return models;
        }

        #endregion

    }
}
