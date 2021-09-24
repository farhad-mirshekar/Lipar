using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
   public interface IFaqGroupService
    {
        void Add(FaqGroup model);
        void Edit(FaqGroup model);
        void Delete(FaqGroup model);
        FaqGroup GetById(int Id);
        IPagedList<FaqGroup> List(FaqGroupListVM listVM);
    }
}
