using Lipar.Core;
using Lipar.Entities.Domain.Organization;
using Lipar.Web.Areas.Admin.Models.Organization;

namespace Lipar.Web.Areas.Admin.Factories.Organization
{
   public interface IPositionModelFactory
    {
        PositionSearchModel PreparePositionSearchModel();
        PositionModel PreparePositionModel(PositionModel model, Position position);
        PositionListModel PreparePositionListModel(PositionSearchModel searchModel);
    }
}
