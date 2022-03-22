using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql.Indexes;

namespace Survey.Module.Indexes
{
    public class SurveyIndex : MapIndex
    {
        public Boolean Good { get; set; }
        public Boolean Fair { get; set; }
        public Boolean Unsatisfy { get; set; }
        public string Station { get; set; }
        public string Ip { set; get; }
        public string User { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
