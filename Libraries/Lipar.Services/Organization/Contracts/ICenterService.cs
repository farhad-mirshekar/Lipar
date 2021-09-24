using Lipar.Core;
using Lipar.Entities.Domain.Organization;

namespace Lipar.Services.Organization.Contracts
{
    public interface ICenterService
    {
        Center GetById(int Id);
        void Add(Center model);
        void Edit(Center model);
        IPagedList<Center> List(CenterListVM listVM);
    }
}
