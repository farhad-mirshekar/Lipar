using Lipar.Entities.Domain.Organization;
using Lipar.Web.Areas.Admin.Models.Organization;

namespace Lipar.Web.Areas.Admin.Factories
{
    public interface ICommandModelFactory
    {
        CommandModel PrepareCommandModel(CommandModel model, Command command);
        CommandListModel PrepareCommandListModel(CommandSearchModel searchModel);
    }
}
