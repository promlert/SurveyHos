using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using Survey.Module.Models;
using Survey.Module.Settings;
using Survey.Module.ViewModels;

namespace Survey.Module.Drivers
{
    public class SurveyPartDisplayDriver : ContentPartDisplayDriver<SurveyPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public SurveyPartDisplayDriver(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public override IDisplayResult Display(SurveyPart part, BuildPartDisplayContext context)
        {
            return Initialize<SurveyPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content:10")
                .Location("Summary", "Content:10")
                ;
        }

        public override IDisplayResult Edit(SurveyPart part, BuildPartEditorContext context)
        {
            return Initialize<SurveyPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Show = part.Show;
                model.ContentItem = part.ContentItem;
                model.SurveyPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(SurveyPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.Show);

            return Edit(model);
        }

        private Task BuildViewModel(SurveyPartViewModel model, SurveyPart part, BuildPartDisplayContext context)
        {
            var settings = context.TypePartDefinition.GetSettings<SurveyPartSettings>();

            model.ContentItem = part.ContentItem;
            model.MySetting = settings.MySetting;
            model.Show = part.Show;
            model.SurveyPart = part;
            model.Settings = settings;

            return Task.CompletedTask;
        }
    }
}
