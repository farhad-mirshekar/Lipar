using System.Collections.Generic;

namespace Lipar.Web.Models.Application
{
    public class ProductQuestionModel : BaseEntityModel
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
        public int ProductId { get; set; }
        public IList<ProductAnswersModel> ProductAnswers { get; set; }
        #endregion
    }
}
