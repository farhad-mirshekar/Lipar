using System;
using System.Collections.Generic;

namespace Lipar.Web.Models.General
{
    public class LanguageSelectorModel
    {
        public LanguageSelectorModel()
        {
            AvailableLanguages = new List<LanguageModel>();
        }
        public IList<LanguageModel> AvailableLanguages { get; set; }
        public int CurrentLanguageId { get; set; }
    }
}
