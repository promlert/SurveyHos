using System;
using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using Survey.Module.Drivers;
using Survey.Module.Handlers;
using Survey.Module.Models;
using Survey.Module.Settings;
using Survey.Module.ViewModels;
using OrchardCore.Modules;
using YesSql.Indexes;
using Survey.Module.Provider;
using OrchardCore.Navigation;

namespace Survey.Module
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<SurveyPartViewModel>();
            });

            services.AddContentPart<SurveyPart>()
                .UseDisplayDriver<SurveyPartDisplayDriver>()
                .AddHandler<SurveyPartHandler>();

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, SurveyPartSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddSingleton<IIndexProvider, SurveyIndexProvider>();
            services.AddSingleton<IIndexProvider, IpIndexProvider>();
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IDataMigration, MigrationsIp>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Survey",
                areaName: "Survey.Module",
                pattern: "Home/Index",
                defaults: new { controller = "Survey", action = "Index" }
            );
        }
    }
}
