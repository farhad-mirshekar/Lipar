using System.Collections.Generic;

namespace Lipar.Web.Models.General
{
    public class MediaListModel
    {
        public MediaListModel()
        {
            AvailableMedia = new List<MediaModel>();
        }
        public IList<MediaModel> AvailableMedia { get; set; }
    }
}
