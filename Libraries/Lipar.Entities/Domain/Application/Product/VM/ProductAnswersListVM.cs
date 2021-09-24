namespace Lipar.Entities.Domain.Application
{
   public class ProductAnswersListVM : BaseListVM
    {
        public int? ProductQuestionId { get; set; }
        public int? UserId { get; set; }
        public int? ViewStatusId { get; set; }
    }
}
