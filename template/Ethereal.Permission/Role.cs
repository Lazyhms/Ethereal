using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.Permission
{
    [SoftDelete]
    [Table("role", Schema = "permission")]
    public class Role : EntityBase
    {
        [Column(TypeName = "nvarchar(50)")]
        [Comment("role name")]
        public string Name { get; set; }
    }
}
