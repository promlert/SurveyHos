using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql.Indexes;

namespace Survey.Module.Indexes
{
    public class IpIndex : MapIndex
    {
        public string Ip { set; get; }
        public string Station { set; get; }
        public string CreateBy { set; get; }
        public DateTime? CreateDate { set; get; }
    }
}
