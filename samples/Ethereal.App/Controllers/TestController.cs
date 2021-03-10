using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Ethereal.App.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Consumes("text/plain")]
        public async Task<object> Post([FromBody] string str)
        {
            var result = Analyzer(str);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Analyzer(string str)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("using System.Collections.Generic;");
            builder.AppendLine(" ");
            builder.AppendLine("public class NewClass {");
            var table = str.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).AsSpan();
            foreach (var item in table)
            {
                var tabs = item.Split("\t", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).AsSpan();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        /// {tabs[3]}");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public {AnalyzerType(tabs[1], tabs[2])} {tabs[0]} {{get;set;}}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }

        private string AnalyzerType(string typeName, string notnull)
        {
            var notnullChar = notnull.IndexOf("Y", StringComparison.OrdinalIgnoreCase) >= 0 ||
                              notnull.IndexOf("是", StringComparison.OrdinalIgnoreCase) >= 0
                              ? ' ' : '?';
            if (typeName.IndexOf("char", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "string";
            }
            else if (typeName.IndexOf("guid", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return $"Guid{notnullChar}";
            }
            else if (typeName.IndexOf("datetime", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return $"DateTime{notnullChar}";
            }
            return $"{typeName}{notnullChar}";
        }
    }
}
