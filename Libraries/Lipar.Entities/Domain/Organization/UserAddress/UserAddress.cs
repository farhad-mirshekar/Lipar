namespace Lipar.Entities.Domain.Organization
{
    public class UserAddress : BaseEntity
    {
        public string PostalCode { get; set; }
        public string Address { get; set; }

        //navigations
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
