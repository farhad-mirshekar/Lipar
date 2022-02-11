using Lipar.Entities.Domain.Core;
using System;

namespace Lipar.Entities.Domain.General
{
    public class UrlRecord : BaseEntity<Guid>
    {
        #region Fields
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string Slug { get; set; }
        public int LanguageId { get; set; }
        #endregion

        #region Navigations
        public Language Language { get; set; }
        #endregion
    }
}
