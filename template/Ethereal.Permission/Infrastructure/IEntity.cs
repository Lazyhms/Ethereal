using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.Permission
{
    public class IEntity
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// create_time
        /// </summary>
        [UpdateIgnore, Comment("create_time")]
        [Column("create_time", TypeName = "datetime", Order = 0)]
        public DateTime Created { get; private set; } = DateTime.Now;

        /// <summary>
        /// update_time
        /// </summary>
        [InsertIgnore, Comment("update_time")]
        [Column("update_time", TypeName = "datetime", Order = 1)]
        public DateTime? Updated { get; set; } = DateTime.Now;
    }
}
