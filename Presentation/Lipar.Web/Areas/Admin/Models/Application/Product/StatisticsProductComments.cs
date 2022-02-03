namespace Lipar.Web.Areas.Admin.Models.Application
{
    public class StatisticsProductComments
    {
        /// <summary>
        /// مجموع کل
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// نظرات تایید شده
        /// </summary>
        public int TotalApprovedComments { get; set; }
        /// <summary>
        /// نظرات تایید نشده
        /// </summary>
        public int TotalNotApprovedComments { get; set; }
    }
}
