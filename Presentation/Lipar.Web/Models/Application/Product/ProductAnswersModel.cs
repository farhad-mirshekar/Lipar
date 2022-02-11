using System;

namespace Lipar.Web.Models.Application
{
    public class ProductAnswersModel : BaseEntityModel<Guid>
    {
        public string AnswerText { get; set; }
        public string FullName { get; set; }
        public Guid ProductQuestionId { get; set; }
    }
}
