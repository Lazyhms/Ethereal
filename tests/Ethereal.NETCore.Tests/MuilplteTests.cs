using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace Ethereal.Json.Tests
{
    public class MuilplteTests
    {
        [Fact]
        public void DateTime_Tests()
        {
            var th = DateTime.Now.Date.AddHours(13).AddMinutes(30).DateDiffMinute(DateTime.Now.Date.AddHours(18));

            var ws = DateTime.Now.FirstDayOfWeek();
            var de = DateTime.Now.LastDayOfWeek();

            var qs = DateTime.Now.FirstDayOfQuarter();
            var qe = DateTime.Now.LastDayOfQuarter();

            var ms = DateTime.Now.FirstDayOfMonth();
            var me = DateTime.Now.LastDayOfMonth();

            var ys = DateTime.Now.FirstDayOfYear();
            var ye = DateTime.Now.LastDayOfYear();
        }

        [Fact]
        public void Json_Tests()
        {
            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
            };
            options.Converters.UseDefaultConverter();
            var j = JsonSerializer.Serialize(new T { }, options);
            var t = JsonSerializer.Deserialize<T>(j, options);
        }

        [Fact]
        public void KeyValuePair()
        {
            var dic = new Dictionary<string, int>() { { "11", 112 } };
            var dic1 = new Dictionary<string, int>() { { "11", 1122 } };
            var t = dic.Union(dic1);
        }

        [Fact]
        public void LeftJoin_Test()
        {
            var posts = new[] { new { PostId = 1, PostTitle = "12333", }, new { PostId = 2, PostTitle = "12333", }, };
            var postTags = new[] { new { PostId = 1, Tag = "HHH" } };

            var result = posts.LeftJoin(postTags, p => p.PostId, pt => pt.PostId, (p, pt) => new { p.PostId, p.PostTitle, pt?.Tag }).ToArray();
        }

        [Fact]
        public void Linq_Tests()
        {
            var _ = new List<T>().Where(1 == 1, t => t.MyProperty is not null);

            var sss = Enumerable.Repeat(new T { IDCard = "132132131" }, 10);
            var sss1 = Enumerable.Repeat(new T { IDCard = "1321321312" }, 10);

            var ttp1 = sss.Concat(sss1).ToPagedList(1, 3);

            var t1 = JsonSerializer.Serialize(ttp1);

            var ttu1 = sss.Union(sss1, (x, y) => Equals(x?.MyProperty, y?.MyProperty)).ToList();

            var ttd1 = sss.Concat(sss1).Distinct().ToList();
            var ttd2 = sss.Concat(sss1).Distinct((x, y) => Equals(x?.IDCard, y?.IDCard)).ToList();
            var ttd3 = EnumerableExtensions.Distinct(sss.Concat(sss1), x => (x?.IDCard)).ToList();
            var ttd4 = EnumerableExtensions.Distinct(sss.Concat(sss1), x => (x?.MyProperty)).ToList();
        }

        [Fact]
        public void Vaild_Tests()
        {
            var t = new T() { IDCard = "321323199107103315", SocialCreditCode = "913201004258014876", Account = "Hu122", Password = "Az12234567" };
            var validationResults = new List<ValidationResult>();
            var ttt1 = Validator.TryValidateObject(t, new ValidationContext(t), validationResults, true);
        }

        [Fact]
        public void Rank_Tests()
        {
            var list1 = new List<int> { 10, 9, 4, 5, 2, 7, 6 }.Concat(Enumerable.Repeat<int>(8, 2)).OrderByDescending(s => s).ToList();
            var rank1 = list1.Rank().ToList();

            var list2 = new List<int?> { 10, 9, 4, 5, 2, 7, 6, null }.Concat(Enumerable.Repeat<int?>(8, 2)).OrderByDescending(s => s).ToList();
            var rank2 = list2.Rank().ToList();

            var list3 = new List<T> { new T { MyProperty4 = 10 }, new T { MyProperty4 = 9 }, new T { MyProperty4 = null }, new T { MyProperty4 = 10 }, new T { MyProperty4 = 8 }, new T { MyProperty4 = 7 }, new T { MyProperty4 = null } };
            var rank3 = list3.Rank(s => s.MyProperty4).ToList();
        }

        [Fact]
        public void Tree_Tests()
        {
            var lists = new List<ST>
            {
                new ST
                {
                     Id = 1L,
                     ParentId=0L
                },
                new ST
                {
                    Id = 2L,
                    ParentId = 1L
                }
            }.ToTreeNode(s => s.Id, s => s.ParentId, 0L).ToList();

            var t = JsonSerializer.Serialize(lists);
        }

        [Fact]
        public void StringBuilder()
        {
            var t1 = new StringBuilder()
                        .Append(1 == 1, "www")
                        .Append(1 == 1, 2)
                        .Append(1 == 2, 2L)
                        .Append(1 == 2, 3D)
                        .Append(EE.A, (s, t1) =>
                        {
                            s.Append(EE.A == t1, "aaa")
                             .Append(EE.B == t1, "bbb");
                        });

            Assert.Equal("www2eee", t1.ToString());
        }
    }

    public enum EE
    {
        A,
        B
    }

    public class ST : TreeNode<ST>
    {
        public long Id { get; set; }

        public long ParentId { get; set; }
    }

    internal class T
    {
        public DateTime? MyProperty { get; set; }

        public Guid? MyProperty1 { get; set; }

        [IDCard]
        public string? IDCard { get; set; }

        [SocialCreditCode]
        public string? SocialCreditCode { get; set; }

        [Account]
        public string? Account { get; set; }

        [Password]
        public string? Password { get; set; }

        public decimal? MyProperty4 { get; set; }

        public IEnumerable<int>? MyProperty5 { get; set; }

        [JsonDateTimeOffsetConverter(DateTimeOffsetConverterOptions.AllowSeconds)]
        public DateTimeOffset? MyProperty6 { get; set; } = DateTimeOffset.Now;
    }
}