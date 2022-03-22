using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using Survey.Module.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql.Sql;

namespace Survey.Module
{
    public class MigrationsIp : DataMigration
    {
        public int Create()
        {
         
            SchemaBuilder.CreateMapIndexTable<IpIndex>(table =>
            {
                table.Column<string>(nameof(IpIndex.Ip));
                table.Column<string>(nameof(IpIndex.Station));
                table.Column<string>(nameof(IpIndex.CreateBy));
                table.Column<DateTime>(nameof(IpIndex.CreateDate));
            });
            return 1;
        }
     
    }
}
