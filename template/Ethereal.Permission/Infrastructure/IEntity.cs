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

        [UpdateIgnore]
        [Column(TypeName = "datetime", Order = 0), DefaultValueSql("CURRENT_TIMESTAMP")]
        public DateTime Created { get; private set; }

        [InsertIgnore]
        [Column(TypeName = "datetime", Order = 1)]
        public DateTime? Updated { get; private set; } = DateTime.Now;
    }
}
