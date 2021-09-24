namespace Lipar.Entities.Domain.Application
{
   public class ProductQuestionListVM : BaseListVM
    {
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public int? ViewStatusId { get; set; }
    }
}
