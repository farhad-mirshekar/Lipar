using Lipar.Core;
using Lipar.Entities.Domain.Organization;
using System;

namespace Lipar.Services.Organization.Contracts
{
    /// <summary>
    /// user address service
    /// </summary>
    public interface IUserAddressService
    {
        /// <summary>
        /// add user address method
        /// </summary>
        /// <param name="model"></param>
        void Add(UserAddress model);
        /// <summary>
        /// edit shipping cost product method
        /// </summary>
        /// <param name="model"></param>
        void Edit(UserAddress model);
        /// <summary>
        /// delete user address method
        /// </summary>
        /// <param name="model"></param>
        void Delete(UserAddress model);
        /// <summary>
        /// retrieve user address by id method
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="noTracking">if true,model return no tracking</param>
        /// <returns></returns>
        UserAddress GetById(Guid Id, bool noTracking = false);
        /// <summary>
        /// list user address method
        /// </summary>
        /// <param name="listVM"></param>
        /// <returns></returns>
        IPagedList<UserAddress> List(UserAddressListVM listVM);
    }
}
