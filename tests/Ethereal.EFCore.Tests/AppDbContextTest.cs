using Ethereal.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.EFCore.Tests
{
    public class AppDbContextTest : DbContext
    {
        public AppDbContextTest(DbContextOptions<AppDbContextTest> builder) : base(builder) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);

        public DbSet<Stu> Stus { get; set; }

        public DbSet<Subject> Subjects { get; set; }
    }

    [SoftDelete]
    [Sequence("Order")]
    [Table("t_stu")]
    public class Stu
    {
        public Guid Id { get; set; }

        [Comment("名称")]
        public string Name { get; set; }

        [Comment("创建时间")]
        [UpdateIgnore]
        [DefaultValueSql("GETDATE()")]
        public DateTime? Created { get; set; }

        [Comment("更新时间")]
        public DateTime? Updated { get; set; }

        [Comment("分数")]
        public decimal Score { get; set; }

        public bool IsDeleted { get; set; }

        [SequenceValue("Order")]
        public int Order { get; set; }
    }

    [SoftDelete]
    [Table("t_subject")]
    public class Subject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }
    }
}
