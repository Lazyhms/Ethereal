using Ethereal.Excel.Infrastructure;
using System.Collections.Generic;
using Xunit;

namespace Ethereal.Excel.Test
{
    public class ExcelTests
    {

        [Fact]
        public void Test1()
        {
            IExcelRender render = new ExcelRender();

            render.RenderToExcel(new List<IDictionary<string, object>> { new Dictionary<string, object> { { "Äã´óÒ¯", 1 } } });
        }
    }

    public class T
    {
        public string Name { get; set; }
    }
}
