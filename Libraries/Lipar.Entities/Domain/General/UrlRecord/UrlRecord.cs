using Lipar.Entities.Domain.Core;

namespace Lipar.Entities.Domain.General
{
    public class UrlRecord : BaseEntity
    {
        #region Fields
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public string Slug { get; set; }
        public int EnabledTypeId { get; set; }
        public int LanguageId { get; set; }
        #endregion

        #region Navigations
        public EnabledType EnabledType { get; set; }
        public Language Language { get; set; }
        #endregion
    }
}
