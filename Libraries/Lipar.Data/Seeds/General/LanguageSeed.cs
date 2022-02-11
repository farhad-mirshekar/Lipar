using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.General;
using Lipar.Entities.Domain.General.Enums;
using System;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.General
{
    public class LanguageSeed : BaseSeed<Language>
    {
        public LanguageSeed()
        {
            Items = new List<Language>()
            {
                new Language
                {
                    Id = 1,
                    Name="پارسی" ,
                    UniqueSeoCode = "fa" ,
                    ViewStatusId = (int)ViewStatusEnum.Active,
                    LanguageCultureId = (int)LanguageCultureEnum.Persian,
                }
            };
        }
        public override string ModelName => "Language";

        public override string Schema => "General";

        public override List<Language> Items { get; set; }
    }
}
