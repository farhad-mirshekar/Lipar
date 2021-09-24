using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Application
{
   public class ProductQuestion : BaseEntity
    {
        #region Ctor
        public ProductQuestion()
        {
            ProductAnswers = new HashSet<ProductAnswers>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// gets or sets the question text
        /// </summary>
        public string QuestionText { get; set; }
        /// <summary>
        /// gets or sets the view status view
        /// </summary>
        public int ViewStatusId { get; set; }
        /// <summary>
        /// gets or sets the product
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// gets or sets the creator question
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region Navigations
        public User User { get; set; }
        public Product Product { get; set; }
        public ViewStatus ViewStatus { get; set; }
        public ICollection<ProductAnswers> ProductAnswers { get; set; }
        #endregion
    }
}
