using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Survey.Module.Settings
{
    public class SurveyPartSettingsViewModel
    {
        public string MySetting { get; set; }

        [BindNever]
        public SurveyPartSettings SurveyPartSettings { get; set; }
    }
}
