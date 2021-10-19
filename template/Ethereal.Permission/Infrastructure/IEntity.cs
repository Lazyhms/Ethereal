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
        [UpdateIgnore, Comment("gmt_create")]
        [Column("gmt_create", TypeName = "datetime", Order = 98)]
        public DateTime Created { get; private set; } = DateTime.Now;

        /// <summary>
        /// update_time
        /// </summary>
        [InsertIgnore, Comment("gmt_modified")]
        [Column("gmt_modified", TypeName = "datetime", Order = 99)]
        public DateTime? Modified { get; private set; } = DateTime.Now;
    }
}
