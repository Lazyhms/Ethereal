using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.App
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options) => Database.EnsureCreated();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test2>().Property("Name").HasColumnType("varchar(100)");
            modelBuilder.Entity<Test2>().Property("Name").HasComment("名称_1");
        }

        public DbSet<Test1> Test1 { get; set; }
    }

    [Table("T_Test1")]
    public class Test1
    {
        [Comment("主键")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "varchar(50)")]
        [Comment("名称")]
        public string Name { get; set; }
    }

    [Table("T_Test2")]
    public class Test2
    {
        [Comment("主键")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        [Comment("名称")]
        public string Name { get; set; }
    }
}