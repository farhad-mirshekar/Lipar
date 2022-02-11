using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Areas.Admin.Models.Organization
{
    public class CommandModel : BaseEntityModel<Guid>
    {
        #region Ctor
        public CommandModel()
        {
            AvailableCommandType = new List<SelectListItem>();
            AvailableCommands = new List<SelectListItem>();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string TitleCrumb { get; set; }
        public Guid? ParentId { get; set; }
        public int CommandTypeId { get; set; }

        #endregion

        #region Select
        public IList<SelectListItem> AvailableCommandType { get; set; }
        public IList<SelectListItem> AvailableCommands { get; set; }
        #endregion
    }
}
