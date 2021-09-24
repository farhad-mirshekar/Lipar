namespace Lipar.Entities
{
    public class BaseListVM : BaseEntity
    {
        public BaseListVM()
        {
            PageIndex = 0;
            PageSize = int.MaxValue;
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
