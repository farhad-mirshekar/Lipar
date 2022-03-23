using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class ProductQuestionModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public ProductQuestionModel()
        {
            ProductAnswersSearchModel = new ProductAnswersSearchModel();
            AvailableViewStatus = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string QuestionText { get; set; }
        public int ViewStatusId { get; set; }
        public string ViewStatusTitle { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public ProductAnswersSearchModel ProductAnswersSearchModel { get; set; }
        #endregion

        #region Select
        public IList<SelectListItem> AvailableViewStatus { get; set; }
        #endregion
    }
}
