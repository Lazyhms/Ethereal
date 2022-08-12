using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.EFCore.PostgreSQL.Tests
{
    [Table("demand", Schema = "platform")]
    public class Demand
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Column("clinical_research_staging")]
        public int[]? ClinicalResearchStaging { get; set; }

        [Column("corresponding_discipline_ids")]
        public int[]? CorrespondingDisciplineIds { get; set; }

    }

    [Table("corresponding_discipline", Schema = "platform")]
    public class CorrespondingDiscipline
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 学科名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("orders")]
        public int Orders { get; set; }
    }
}