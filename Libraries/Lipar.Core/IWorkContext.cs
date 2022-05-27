using Lipar.Entities.Domain.General;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections.Generic;

namespace Lipar.Core
{
    public interface IWorkContext
    {
        User CurrentUser { get; set; }
        Position CurrentPosition { get; set; }
        IEnumerable<Position> Positions { get; }
        IEnumerable<Command> Commands { get; set; }
        Center CurrentCenter { get; }
        /// <summary>
        /// get shopping cart item  by cookie
        /// </summary>
        Guid? ShoppingCartItems { get; set; }
        /// <summary>
        /// get or set working language
        /// </summary>
        Language WorkingLanguage { get; set; }
    }
}
