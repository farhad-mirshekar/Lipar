using Lipar.Entities.Domain.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Entities.Domain.Portal
{
    /// <summary>
    /// گالری تصاویر
    /// </summary>
    public class Gallery : BaseEntity
    {
        #region Ctor
        public Gallery()
        {

        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string Body { get; set; }
        public int ViewStatusId { get; set; }
        public int VisitedCount { get; set; }
        public int UserId { get; set; }
        public string MetaKeywords { get; set; }
        public int? RemoverId { get; set; }
        public DateTime? RemoveDate { get; set; }
        #endregion

        #region Navigations
        public User User { get; set; }
        public User Remover { get; set; }
        public ViewStatus ViewStatus { get; set; }
        #endregion
    }
}
