using Lipar.Core;
using Lipar.Entities.Domain.Application;

namespace Lipar.Services.Application.Contracts
{
   public interface IProductAttributeMappingService
    {
        /// <summary>
        /// add product attribute mapping method
        /// </summary>
        /// <param name="model"></param>
        void Add(ProductAttributeMapping model);
        /// <summary>
        /// edit product attribute mapping method
        /// </summary>
        /// <param name="model"></param>
        void Edit(ProductAttributeMapping model);
        /// <summary>
        /// delete product attribute model method
        /// </summary>
        /// <param name="model"></param>
        void Delete(ProductAttributeMapping model);
        /// <summary>
        /// retrieve product attribute mapping by id method
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductAttributeMapping GetById(int Id);
        /// <summary>
        /// list product attribute mapping method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<ProductAttributeMapping> List(ProductAttributeMappingListVM listVM);
    }
}
