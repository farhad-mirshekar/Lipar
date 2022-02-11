using System;

namespace Lipar.Entities.Domain.Application
{
   public class ProductQuestionListVM : BaseListVM
    {
        public Guid? ProductId { get; set; }
        public Guid? UserId { get; set; }
        public int? ViewStatusId { get; set; }
    }
}
