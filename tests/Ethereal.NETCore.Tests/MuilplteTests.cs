using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ImageSharp;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace Ethereal.Json.Tests
{
    public class MuilplteTests
    {
        private readonly List<T> sss = Enumerable.Repeat(new T { MyProperty2 = "132132131" }, 10).ToList();
        private readonly List<T> sss1 = Enumerable.Repeat(new T { MyProperty2 = "1321321312" }, 10).ToList();

        [Fact]
        public void DateTime_Tests()
        {
            var th = DateTime.Now.Date.AddHours(13).AddMinutes(30).DateDiffMinute(DateTime.Now.Date.AddHours(18));

            var dq = DateTime.Now.DateInQuarter();

            var dqs = DateTime.Now.DateInQuarter().GetDescription();

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
        public void Linq_Tests()
        {
            new List<T>().Where(1 == 1, t => t.MyProperty is not null);

            var ttu1 = sss.Union(sss1).ToList();
            var ttu2 = sss.Union(sss1, (x, y) => Equals(x?.MyProperty, y?.MyProperty)).ToList();

            var ttd1 = sss.Union(sss1).Distinct().ToList();
            var ttd2 = sss.Union(sss1).Distinct((x, y) => Equals(x?.MyProperty2, y?.MyProperty2)).ToList();
            var ttd3 = sss.Union(sss1).Distinct(x => x?.MyProperty2).ToList();
            var ttd4 = sss.Union(sss1).Distinct(x => x?.MyProperty).ToList();
        }

        [Fact]
        public void LeftJoin_Test()
        {
            var posts = new[] { new { PostId = 1, PostTitle = "12333", }, new { PostId = 2, PostTitle = "12333", }, };
            var postTags = new[] { new { PostId = 1, Tag = "HHH" } };

            var result = posts.LeftJoin(postTags, p => p.PostId, pt => pt.PostId, (p, pt) => new { p.PostId, p.PostTitle, pt?.Tag }).ToArray();
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
        public void Vaild_Tests()
        {
            var t = new T() { MyProperty2 = "3213231991071033115", MyProperty3 = "913201004258014876" };
            var validationResults = new List<ValidationResult>();
            var ttt1 = Validator.TryValidateObject(t, new ValidationContext(t), validationResults, true);
        }

        [Fact]
        public void KeyValuePair()
        {
            var dic = new Dictionary<string, int>() { { "11", 112 } };
            var dic1 = new Dictionary<string, int>() { { "11", 1122 } };
            var t = dic.Union(dic1);


        }

        [Fact]
        public async Task ImageSharp_TestsAsync()
        {
            var sharp = new ImageSharp();

            await sharp.ResizeAsync("7946170535396804.jpg", "1.jpg");

            await sharp.MergeImageAsync("7946170535396804.jpg", "1.jpg", "2.jpg", 200, 300, 3);

            return await sharp.MergeImageAsync(new MemoryStream(), "", 1, 1, new JpegEncoder());
        }
    }

    internal class T
    {
        public DateTime? MyProperty { get; set; }

        public Guid? MyProperty1 { get; set; }

        [IDCard]
        public string? MyProperty2 { get; set; }

        [SocialCreditCode]
        public string? MyProperty3 { get; set; }

        public decimal? MyProperty4 { get; set; }

        public IEnumerable<int>? MyProperty5 { get; set; }

        [JsonDateTimeOffsetConverter(DateTimeOffsetConverterOptions.AllowSeconds)]
        public DateTimeOffset? MyProperty6 { get; set; } = DateTimeOffset.Now;
    }
}
