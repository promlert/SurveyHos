using OrchardCore.ContentManagement.Handlers;
using System.Threading.Tasks;
using Survey.Module.Models;

namespace Survey.Module.Handlers
{
    public class SurveyPartHandler : ContentPartHandler<SurveyPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, SurveyPart part)
        {
            part.Show = true;

            return Task.CompletedTask;
        }
    }
}