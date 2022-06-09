namespace Lipar.Web.Models.Common
{
    public class UserHeaderLinkModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserInfo { get; set; }
        public int UserTypeId { get; set; }
        public int GenderId { get; set; }
    }
}
