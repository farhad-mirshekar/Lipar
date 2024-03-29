﻿using Lipar.Core;
using Lipar.Entities.Domain.General;
using System;

namespace Lipar.Services.General.Contracts
{
    public interface ILanguageService
    {
        void Add(Language model);
        void Edit(Language model);
        Language GetById(int Id, bool noTracking = false);
        void Delete(Language model);
        IPagedList<Language> List(LanguageListVM listVM);
    }
}
