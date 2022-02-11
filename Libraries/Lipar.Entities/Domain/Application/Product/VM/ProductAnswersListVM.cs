using System;

namespace Lipar.Entities.Domain.Application
{
   public class ProductAnswersListVM : BaseListVM
    {
        public Guid? ProductQuestionId { get; set; }
        public Guid? UserId { get; set; }
        public int? ViewStatusId { get; set; }
    }
}
