using System.Collections.Generic;
using System.Linq;

namespace Lipar.Web.Framework.UI
{
    public class PageHeadBuilder : IPageHeadBuilder
    {
        #region Fields
        private string _activeMenuItemSystemName;
        private IList<string> _metaDescriptions;
        #endregion

        #region Ctor
        public PageHeadBuilder()
        {
            _metaDescriptions = new List<string>();
        }
        #endregion

        #region Methods
        public string GetActiveMenuItemSystemName()
        => _activeMenuItemSystemName;

        public void SetActiveMenuItemSystemName(string systemName)
        {
            _activeMenuItemSystemName = systemName;
        }
        public void AddMetaDescription(string metaDescription)
        {
            if (string.IsNullOrWhiteSpace(metaDescription))
                return;

            _metaDescriptions.Add(metaDescription.Trim());
        }

        public string GenerateMetaDescription()
        {
            var result = string.Join(",", _metaDescriptions.AsEnumerable().Reverse().ToArray());
            return !string.IsNullOrEmpty(result) ? result : result;
        }

        public void AppendMetaDescription(string metaDescription)
        {
            if (string.IsNullOrWhiteSpace(metaDescription))
                return;

            _metaDescriptions.Insert(0,metaDescription.Trim());
        }
        #endregion
    }
}
