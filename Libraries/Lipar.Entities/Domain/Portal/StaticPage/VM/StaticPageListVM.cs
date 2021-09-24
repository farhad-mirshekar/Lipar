namespace Lipar.Entities.Domain.Portal
{
   public class StaticPageListVM : BaseListVM
    {
        #region Ctor
        public StaticPageListVM()
        {
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public int? ViewStatusId { get; set; }
        public bool? IncludeInTopMenu { get; set; }
        #endregion
    }
}
