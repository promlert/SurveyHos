using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using Survey.Module.Models;
using Survey.Module.Settings;

namespace Survey.Module.ViewModels
{
    public class SurveyPartViewModel
    {
        public string MySetting { get; set; }

        public bool Show { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public SurveyPart SurveyPart { get; set; }

        [BindNever]
        public SurveyPartSettings Settings { get; set; }
    }
}
