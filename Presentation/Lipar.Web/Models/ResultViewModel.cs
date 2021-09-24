namespace Lipar.Web.Models
{
    public class ResultViewModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// type for noty popup such as : alert, success, warning, error, info/information
        /// </summary>
        public string NotyType { get; set; }
        public bool Clear { get; set; }
        public string DivName { get; set; }
    }
}
