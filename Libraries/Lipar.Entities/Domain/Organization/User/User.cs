using Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Financial;
using Lipar.Entities.Domain.General;
using System;
using System.Collections.Generic;
using Application = Lipar.Entities.Domain.Application;
using Portal = Lipar.Entities.Domain.Portal;

namespace Lipar.Entities.Domain.Organization
{
    public class User : BaseEntity<Guid>
    {
        #region Ctor
        public User()
        {
            Positions = new HashSet<Position>();
            UserPasswords = new HashSet<UserPassword>();
            ApplicationCategories = new HashSet<Application.Category>();
            Products = new HashSet<Application.Product>();
            ProductAnswers = new HashSet<Application.ProductAnswers>();
            ProductComments = new HashSet<Application.ProductComment>();
            ProductQuestions = new HashSet<Application.ProductQuestion>();
            Blogs = new HashSet<Portal.Blog>();
            BlogComments = new HashSet<Portal.BlogComment>();
            PortalCategories = new HashSet<Portal.Category>();
            DynamicPages = new HashSet<Portal.DynamicPage>();
            DynamicPageDetails = new HashSet<Portal.DynamicPageDetail>();
            Galleries = new HashSet<Portal.Gallery>();
            News = new HashSet<Portal.News>();
            NewsComments = new HashSet<Portal.NewsComment>();
            StaticPages = new HashSet<Portal.StaticPage>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            Banks = new HashSet<Bank>();
            UserAddresses = new HashSet<UserAddress>();
            Orders = new HashSet<Order>();
            OrderPaymentStatuses = new HashSet<OrderPaymentStatus>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets enable type
        /// </summary>
        public int EnabledTypeId { get; set; }
        /// <summary>
        /// gets or sets the user guid
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// gets or sets the username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// gets or sets the user type
        /// </summary>
        public int UserTypeId { get; set; }
        /// <summary>
        /// gets or sets the first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// gets or sets the last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// gets or sets the national code
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// gets or sets the gender
        /// </summary>
        public int GenderId { get; set; }
        /// <summary>
        /// gets or sets the email
        /// </summary>
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string CellPhone { get; set; }
        public bool CellPhoneVerified { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? FailedLoginAttempts { get; set; }
        public DateTime? CannotLoginUntilDate { get; set; }
        public Guid? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public UserType UserType { get; set; }
        public EnabledType EnabledType { get; set; }
        public Gender Gender { get; set; }
        public ICollection<UserPassword> UserPasswords { get; set; }
        public ICollection<Position> Positions { get; set; }
        public ICollection<Application.Category> ApplicationCategories { get; set; }
        public ICollection<Application.Product> Products { get; set; }
        public ICollection<Application.ProductAnswers> ProductAnswers { get; set; }
        public ICollection<Application.ProductComment> ProductComments { get; set; }
        public ICollection<Application.ProductQuestion> ProductQuestions { get; set; }
        public ICollection<Portal.Blog> Blogs { get; set; }
        public ICollection<Portal.BlogComment> BlogComments { get; set; }
        public ICollection<Portal.Category> PortalCategories { get; set; }
        public ICollection<Portal.DynamicPage> DynamicPages { get; set; }
        public ICollection<Portal.DynamicPageDetail> DynamicPageDetails { get; set; }
        public ICollection<Portal.Gallery> Galleries { get; set; }
        public ICollection<Portal.News> News { get; set; }
        public ICollection<Portal.NewsComment> NewsComments { get; set; }
        public ICollection<Portal.StaticPage> StaticPages { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ICollection<Bank> Banks { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Application.OrderPaymentStatus> OrderPaymentStatuses { get; set; }
        #endregion
    }
}
