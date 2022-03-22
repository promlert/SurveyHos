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
    public class SurveyIndexProvider : IndexProvider<SurveyModel>
    {
        public override void Describe(DescribeContext<SurveyModel> context)
        {
            context.For<SurveyIndex>()
               .Map(fund =>
                   new SurveyIndex
                   {
                       Good = fund.Good,
                       Unsatisfy = fund.Unsatisfy,
                       Fair = fund.Fair,
                       User = fund.User,
                       Station = fund.Station,
                       Ip = fund.Ip,
                       CreateDate = fund.CreateDate
                   });
            //context.For<SurveyGroupIndex>()
            //    .Map(survey =>
            //       new SurveyGroupIndex
            //       {
            //           CreateDte = survey.CreateDate.ToString("yyyyMMdd"),
            //           Fair = survey.Fair ,
            //           Good = survey.Good,
            //           Unsatisfy = survey.Unsatisfy,
            //           User = survey.User,
            //       });
            //    .Group(survey => survey.CreateDte).Reduce(group =>
            //       new SurveyGroupIndex
            //       {
            //           CreateDte = group.Key,
            //           Good = group.Sum(p => p.Good),
            //           Fair = group.Sum(p => p.Fair),
            //           Unsatisfy = group.Sum(p => p.Unsatisfy)
            //       });
            //.Delete((index, map) =>
            //{
            //      index.Count -= map.Sum(x => x.Count);

            //    // if Count == 0 then delete the index
            //    return index;
            //});
        }
    }
}
