using Lipar.Web.Framework.Models;
using System;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductAnswersSearchModel : BaseSearchModel
    {
        public Guid? ProductQuestionId { get; set; }
    }
}
