using Lipar.Entities.Domain.General;
using System.Collections.Generic;

namespace Lipar.Data.Seeds.General
{
    public class LanguageCultureSeed : BaseSeed<LanguageCulture>
    {
        public LanguageCultureSeed()
        {
            Items = new List<LanguageCulture>()
            {
                new LanguageCulture{ Id = 1, Title="English",Seo=""},
                new LanguageCulture{ Id = 2, Title="French",Seo=""},
                new LanguageCulture{ Id = 3, Title="German",Seo=""},
                new LanguageCulture{ Id = 4, Title="Turkish",Seo=""},
                new LanguageCulture{ Id = 5, Title="Spanish",Seo=""},
                new LanguageCulture{ Id = 6, Title="Persian",Seo=""},
                new LanguageCulture{ Id = 7, Title="Arabic",Seo=""},
                new LanguageCulture{ Id = 8, Title="Hindi",Seo=""},
                new LanguageCulture{ Id = 9, Title="Chinese",Seo=""},

            };
        }
        public override string ModelName => "LanguageCulture";

        public override string Schema => "General";

        public override List<LanguageCulture> Items { get; set; }
    }
}
