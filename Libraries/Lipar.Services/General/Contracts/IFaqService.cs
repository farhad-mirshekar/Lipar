using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    public interface IFaqService
    {
        void Add(Faq model);
        void Edit(Faq model);
        void Delete(Faq model);
        Faq GetById(int Id);
        IPagedList<Faq> List(FaqListVM listVM);
    }
}
