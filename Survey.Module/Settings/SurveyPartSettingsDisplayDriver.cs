using System;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using Survey.Module.Models;

namespace Survey.Module.Settings
{
    public class SurveyPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!String.Equals(nameof(SurveyPart), contentTypePartDefinition.PartDefinition.Name))
            {
                return null;
            }

            return Initialize<SurveyPartSettingsViewModel>("SurveyPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<SurveyPartSettings>();

                model.MySetting = settings.MySetting;
                model.SurveyPartSettings = settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(SurveyPart), contentTypePartDefinition.PartDefinition.Name))
            {
                return null;
            }

            var model = new SurveyPartSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.MySetting))
            {
                context.Builder.WithSettings(new SurveyPartSettings { MySetting = model.MySetting });
            }

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}
