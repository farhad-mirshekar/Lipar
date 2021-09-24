using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductAnswersModel : BaseEntityModel
    {
        #region Ctor
        public ProductAnswersModel()
        {
            AvailableViewStatus = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string AnswerText { get; set; }
        public int ProductQuestionId { get; set; }
        public int ViewStatusId { get; set; }
        public string ViewStatusTitle { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableViewStatus { get; set; }
        #endregion
    }
}
