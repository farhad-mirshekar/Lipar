using Lipar.Core;
using Lipar.Entities.Domain.General;

namespace Lipar.Services.General.Contracts
{
    public interface ILanguageService
    {
        void Add(Language model);
        void Edit(Language model);
        Language GetById(int Id);
        void Delete(Language model);
        IPagedList<Language> List(LanguageListVM listVM);
    }
}
