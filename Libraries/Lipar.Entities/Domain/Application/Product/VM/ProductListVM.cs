namespace Lipar.Entities.Domain.Application
{
    public class ProductListVM : BaseListVM
    {
        public ProductListVM()
        {
            //ProductSortingType = ProductSortingType.CreationDateDesc;
        }
        public string Name { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        //public ProductSortingType ProductSortingType { get; set; }
    }
}
