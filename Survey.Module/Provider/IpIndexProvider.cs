using Survey.Module.Indexes;
using Survey.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql.Indexes;

namespace Survey.Module.Provider
{
    public class IpIndexProvider : IndexProvider<IpModel>
    {
        public override void Describe(DescribeContext<IpModel> context)
        {
            context.For<IpIndex>()
               .Map(fund =>
                   new IpIndex
                   {
                   //    Id = fund.Id,
                       Ip = fund.Ip,
                       CreateDate = fund.CreateDate,
                       CreateBy = fund.CreateBy,
                       Station = fund.Station
                   });
        }
    }
}
