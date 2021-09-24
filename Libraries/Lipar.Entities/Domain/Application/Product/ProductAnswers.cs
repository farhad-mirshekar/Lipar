﻿using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;

namespace Lipar.Entities.Domain.Application
{
    public class ProductAnswers : BaseEntity
    {
        /// <summary>
        /// gets or sets the answer text 
        /// </summary>
        public string AnswerText { get; set; }
        /// <summary>
        /// gets or sets the product question id
        /// </summary>
        public int ProductQuestionId { get; set; }

        /// <summary>
        /// gets or sets the view status type
        /// </summary>
        public int ViewStatusId { get; set; }
        /// <summary>
        /// gets or sets the creator answer
        /// </summary>
        public int UserId { get; set; }

        #region Navigations
        public User User { get; set; }
        public ProductQuestion ProductQuestion { get; set; }
        public ViewStatus ViewStatus { get; set; }
        #endregion
    }
}
