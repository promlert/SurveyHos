using System.Collections.Generic;
using OrchardCore.Queries;

namespace OrchardCore.Lucene
{
    public class LuceneQueryResults : IQueryResults
    {
        public IEnumerable<object> Items { get; set; }
        public int Count { get; set; }
    }
}
