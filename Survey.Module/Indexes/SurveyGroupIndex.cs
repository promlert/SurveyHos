using System;
using YesSql.Indexes;

namespace Survey.Module.Indexes
{
    public class SurveyGroupIndex
    {
        public string User { set; get; }
        public DateTime CreateDte { set; get; }
        public string Ip { get; set; }
        public string Station { get; set; }
        public bool Good { set; get; }
        public bool Fair { get; set; }
        public bool Unsatisfy { get; set; }

    }
}
