namespace Lipar.Web.Models.Application
{
    public class ProductAnswersModel : BaseEntityModel
    {
        public string AnswerText { get; set; }
        public string FullName { get; set; }
        public int ProductQuestionId { get; set; }
    }
}
