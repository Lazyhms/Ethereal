﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal.EFCore.Tests
{
    public class AppDbContextTest : DbContext
    {
        public AppDbContextTest(DbContextOptions<AppDbContextTest> builder) : base(builder) => Database.EnsureCreated();

        public DbSet<Stu> Stus { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    [SoftDelete]
    [Table("t_stu")]
    public class Stu
    {
        [Comment("创建时间")]
        [UpdateIgnore]
        public DateTime? Created { get; set; }

        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [Comment("名称")]
        public string Name { get; set; }

        public int Order { get; set; }

        [Comment("分数")]
        public decimal Score { get; set; }

        [Comment("更新时间")]
        public DateTime? Updated { get; set; }

        public Guid? SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }

    [SoftDelete]
    [Table("t_subject")]
    public class Subject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}