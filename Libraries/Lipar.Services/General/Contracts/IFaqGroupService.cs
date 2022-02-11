using Lipar.Core;
using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Services.General.Contracts
{
   public interface IFaqGroupService
    {
        void Add(FaqGroup model);
        void Edit(FaqGroup model);
        void Delete(FaqGroup model);
        FaqGroup GetById(Guid Id);
        IPagedList<FaqGroup> List(FaqGroupListVM listVM);
    }
}
