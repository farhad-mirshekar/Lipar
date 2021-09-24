namespace Lipar.Entities.Domain.Portal
{
   public class DynamicPageListVM : BaseListVM
    {
        public string Name { get; set; }
        public bool? IncludeInTopMenu { get; set; }
        public int ViewStatusId { get; set; }
    }
}
