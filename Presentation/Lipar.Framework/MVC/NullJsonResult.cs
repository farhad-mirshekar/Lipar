using Microsoft.AspNetCore.Mvc;

namespace Lipar.Web.Framework.MVC
{
    public class NullJsonResult : JsonResult
    {
        public NullJsonResult() : base(null)
        {

        }
    }
}
