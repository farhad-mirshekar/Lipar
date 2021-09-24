using Lipar.Entities.Configuration;

namespace Lipar.Entities.Domain.Portal
{
    /// <summary>
    /// blog settings
    /// </summary>
   public class BlogSetting : ISettings
    {
        /// <summary>
        /// page size 
        /// </summary>
        public int BlogPageSize { get; set; }
    }
}
