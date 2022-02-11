using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class ProductQuestionModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public ProductQuestionModel()
        {
            ProductAnswers = new List<ProductAnswersModel>();
        }
        #endregion

        #region Fields
        public string QuestionText { get; set; }
        public string FullName { get; set; }
        public Guid ProductId { get; set; }
        public IList<ProductAnswersModel> ProductAnswers { get; set; }
        #endregion
    }
}
