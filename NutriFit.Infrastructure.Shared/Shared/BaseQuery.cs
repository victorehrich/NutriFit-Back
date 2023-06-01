using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Shared.Shared
{
    public abstract class BaseQuery
    {
        public string Table { get; set; }
        public List<string> InnerTable { get; set; }
        public string Query { get; set; }
        public object Parameters { get; set; }
    }
}
