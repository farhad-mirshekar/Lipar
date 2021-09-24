using Lipar.Entities.Domain.General;

namespace Lipar.Entities.Domain.Portal
{
    public class NewsMedia
    {
        public News News { get; set; }
        public int NewsId { get; set; }

        public Media Media { get; set; }
        public int MediaId { get; set; }
    }
}
