using Lipar.Web.Framework.MVC.Filters;
using Microsoft.AspNetCore.Mvc;
using Lipar.Web.Framework.Models;

namespace Lipar.Web.Framework.Controllers
{
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    [AuthorizeAdmin]
    public class BaseAdminController : BaseController
    {
        public JsonResult Json<T>(BasePagedListModel<T> model) where T : BaseLiparModel
        {
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = model.RecordsTotal,
                recordsFiltered = model.RecordsFiltered,
                Data = model.Data
            });
        }

        protected JsonResult ErrorJson(object errors)
        {
            return Json(new
            {
                error = errors
            });
        }
    }
}
