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
        [Column("gmt_create", TypeName = "datetime")]
        public DateTime Created { get; private set; } = DateTime.Now;

        /// <summary>
        /// update_time
        /// </summary>
        [AddIgnore, Comment("gmt_modified")]
        [Column("gmt_modified", TypeName = "datetime")]
        public DateTime? Modified { get; private set; } = DateTime.Now;
    }
}
