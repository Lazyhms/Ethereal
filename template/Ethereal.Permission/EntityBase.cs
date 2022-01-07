using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.Permission
{
    public class EntityBase
    {
        [Key]
        [Column("id")]
        [Comment("Identity")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// create_time
        /// </summary>
        [UpdateIgnore, Comment("gmt_create")]
        [Column("gmt_create")]
        public DateTime Created { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// update_time
        /// </summary>
        [AddIgnore, Comment("gmt_modified")]
        [Column("gmt_modified")]
        public DateTime? Modified { get; private set; } = DateTime.UtcNow;
    }
}
