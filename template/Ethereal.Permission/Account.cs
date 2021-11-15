using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.Permission
{
    [SoftDelete]
    [Table("Account", Schema = "User")]
    public class Account : IEntity
    {
        /// <summary>
        /// 联合主键
        /// </summary>
        [Column(Order = 1)]
        public long UnionId { get; set; }
        /// <summary>
        /// 身份类型
        /// </summary>
        [Column(Order = 2)]
        public int IdentityType { get; set; }
        /// <summary>
        /// 标识符
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Certificate { get; set; }
    }
}
