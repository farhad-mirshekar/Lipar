﻿using System;

namespace Lipar.Entities.Domain.General
{
   public class GenericAttribute : BaseEntity<Guid>
    {
        /// <summary>
        /// gets or sets the key group
        /// </summary>
        public string KeyGroup { get; set; }
        /// <summary>
        /// gets or sets the key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// gets or sets the value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// gets or sets the entity id
        /// </summary>
        public string EntityId { get; set; }
    }
}
