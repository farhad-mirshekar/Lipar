namespace Lipar.Web.Models.Financial
{
    public class BankPortModel : BaseEntityModel
    {
        public int BankId { get; set; }
        public string MerchantId { get; set; }
        public string Name { get; set; }
        public string MerchantKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TerminalId { get; set; }
    }
}
