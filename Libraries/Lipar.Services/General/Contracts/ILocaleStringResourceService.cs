using Lipar.Core;
using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Services.General.Contracts
{
    public interface ILocaleStringResourceService
    {
        void Add(LocaleStringResource model);
        void Edit(LocaleStringResource model);
        LocaleStringResource GetById(Guid Id);
        LocaleStringResource GetByResourceName(string ResourceName, int LanguageId);
        string GetResource(string Format);
        void Delete(LocaleStringResource model);
        IPagedList<LocaleStringResource> List(LocaleStringResourceListVM listVM);
    }
}
