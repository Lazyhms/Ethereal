using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Ethereal.NETCore.Tests
{
    public interface Tree<T, TK> where T : class
                                 where TK : struct
    {
        public TK Id { get; set; }

        public TK ParentId { get; set; }

        public IList<Tree<T, TK>> Children { get; set; }
    }

    public class Stu : Tree<Stu, int>
    {
        [Required]
        public int Id { get; set; }

        public int ParentId { get; set; }

        [Required, IDCard]
        public string? IDCard { get; set; }

        public IList<Tree<Stu, int>> Children { get; set; } = new List<Tree<Stu, int>>();
    }

    public static class Ext
    {
        public static IList<Tree<T, TK>> Tree<T, TK>(this IEnumerable<Tree<T, TK>> tree) where T : class
                                                                                         where TK : struct
        {
            var menus = tree.ToDictionary(s => s.Id, s => s);
            foreach (var value in menus.Values)
            {
                if (menus.ContainsKey(value.ParentId))
                {
                    menus[value.ParentId].Children.Add(value);
                }
            }
            return menus.Values.Where(m => Equals(0, m.ParentId)).ToList();
        }
    }

    public class Test
    {
        [Fact]
        public void T_Test()
        {
            var stus = new List<Stu>
            {
                new Stu{ Id=1,ParentId=0},
                new Stu{ Id=2,ParentId=1}
            };

            var t = stus.Tree().ToList();
        }

    }
}
