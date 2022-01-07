using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.Permission
{
    [SoftDelete]
    [Table("role", Schema = "permission")]
    public class Role : EntityBase
    {
        [Comment("role name")]
        public string Name { get; set; }
    }
}
