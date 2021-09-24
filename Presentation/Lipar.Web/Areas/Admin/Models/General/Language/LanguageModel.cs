using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.General
{
    public class LanguageModel : BaseEntityModel
    {
        public LanguageModel()
        {
            AvailableLanguageCultureType = new List<SelectListItem>();
            AvailableViewStatusType = new List<SelectListItem>();
            LocaleResourceSearchModel = new LocaleStringResourceSearchModel();
        }
        public string Name { get; set; }
        public int LanguageCultureId { get; set; }
        public string UniqueSeoCode { get; set; }
        public int ViewStatusId { get; set; }
        public LocaleStringResourceSearchModel LocaleResourceSearchModel { get; set; }

        #region Select
        public IList<SelectListItem> AvailableLanguageCultureType { get; set; }
        public IList<SelectListItem> AvailableViewStatusType { get; set; }
        #endregion
    }
}
