using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories
{
    public class CommandModelFactory : ICommandModelFactory
    {
        #region Fields
        private readonly ICommandService _commandService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public CommandModelFactory(ICommandService commandService
                                 , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _commandService = commandService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Methods
        public CommandListModel PrepareCommandListModel(CommandSearchModel searchModel)
        {
            var commands = _commandService.List(new CommandListVM {PageIndex = searchModel.Page - 1 , PageSize = searchModel.PageSize , Name = searchModel.Name });

            var model = new CommandListModel().PrepareToGrid(searchModel, commands, () =>
            {
                return commands.Select(command =>
                {
                    var commandModel = command.ToModel<CommandModel, Guid>();

                    commandModel.TitleCrumb = _commandService.GetFormattedBreadCrumb(command) ?? "";
                    return commandModel;
                });
            });

            return model;
        }

        public CommandModel PrepareCommandModel(CommandModel model, Command command)
        {
            if (command != null)
                model = command.ToModel<CommandModel, Guid>();

            _baseAdminModelFactory.PrepareCommandType(model.AvailableCommandType);
            model.AvailableCommands = _baseAdminModelFactory.PrepareCommand();

            return model;
        }
        #endregion
    }
}
