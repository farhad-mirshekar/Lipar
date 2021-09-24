namespace Lipar.Web.Areas.Admin.Models.General
{
    public class FaqModel : BaseEntityModel
    {
        public int FaqGroupId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
