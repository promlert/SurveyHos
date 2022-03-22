using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using Survey.Module.Indexes;
using YesSql.Sql;
using System;

namespace Survey.Module
{
    public class Migrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition("SurveyPart", builder => builder
                .Attachable()
                .WithDescription("Provides a Survey part for your content item."));
            SchemaBuilder.CreateMapIndexTable<SurveyIndex>(table =>
            {

                table.Column<Boolean>(nameof(SurveyIndex.Good));
                table.Column<Boolean>(nameof(SurveyIndex.Fair));
                table.Column<Boolean>(nameof(SurveyIndex.Unsatisfy));
                table.Column<string>(nameof(SurveyIndex.Station));
                table.Column<string>(nameof(SurveyIndex.User));
                table.Column<DateTime>(nameof(SurveyIndex.CreateDate));
            });
            return 2;
        }
        public int UpdateFrom1()
        {
            SchemaBuilder.AlterTable(nameof(SurveyIndex), table =>
            {
                table.AddColumn<Boolean>(nameof(SurveyIndex.Good));
                table.AddColumn<Boolean>(nameof(SurveyIndex.Fair));
                table.AddColumn<Boolean>(nameof(SurveyIndex.Unsatisfy));
                table.AddColumn<string>(nameof(SurveyIndex.Station));
                table.AddColumn<string>(nameof(SurveyIndex.User));
                table.AddColumn<DateTime>(nameof(SurveyIndex.CreateDate));
            });
            return 2;
        }
    }
}
