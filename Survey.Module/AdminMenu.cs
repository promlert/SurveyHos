using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace Survey.Module
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder.Add(T["แบบสอบถาม"], "11", fxiaokeCrm =>
            {
                fxiaokeCrm.Add(T["SurveyMenu"], "0", codeSettings => codeSettings
                    .Action("Index", "Survey", "Survey.Module")
                    .LocalNav());
                fxiaokeCrm.Add(T["StationMenu"], "1", codeSettings => codeSettings
                   .Action("Management", "Survey", "Survey.Module")
                   .LocalNav());
            });

            return Task.CompletedTask;
        }
    }
}