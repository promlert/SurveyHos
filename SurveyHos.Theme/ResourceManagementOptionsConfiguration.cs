using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace SurveyHos.Theme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineStyle("surveyhos-bootstrap-oc")
                .SetUrl("~/SurveyHos.Theme/css/bootstrap-oc.min.css", "~/SurveyHos.Theme/css/bootstrap-oc.css")
                .SetVersion("1.0.0");
            _manifest
             .DefineScript("surveyhos-vendor-jquery")
             .SetUrl("~/SurveyHos.Theme/lib/jquery/jquery.min.js", "~/SurveyHos.Theme/lib/jquery/jquery.js")
             .SetCdn("https://code.jquery.com/jquery.min.js", "https://code.jquery.com/jquery.js")
             .SetCdnIntegrity("sha384-vk5WoKIaW/vJyUAd9n/wmopsmNhiy+L2Z+SBxGYnUkunIxVxAv/UtMOhba/xskxh", "sha384-mlceH9HlqLp7GMKHrj5Ara1+LvdTZVMx4S1U43/NxCvAkzIo8WJ0FE7duLel3wVo")
             .SetVersion("3.4.1");

            _manifest
            .DefineStyle("surveyhos-vendor-font-awesome")
            .SetUrl("~/SurveyHos.Theme/lib/fontawesome-free/css/all.min.css", "~/SurveyHos.Theme/lib/fontawesome-free/css/all.css")
            .SetCdn("https://cdn.jsdelivr.net/npm/@fortawesome/fontawesome-free@5.15.4/css/all.min.css", "https://cdn.jsdelivr.net/npm/@fortawesome/fontawesome-free@5.15.4/css/all.css")
            .SetCdnIntegrity("sha384-rtJEYb85SiYWgfpCr0jn174XgJTn4rptSOQsMroFBPQSGLdOC5IbubP6lJ35qoM9", "sha384-Ex0vLvgbKZTFlqEetkjk2iUgM+H5udpQKFKjBoGFwPaHRGhiWyVI6jLz/3fBm5ht")
            .SetVersion("5.15.4");

            _manifest
              .DefineScript("surveyhos-vendor-datatables")
              .SetUrl("~/SurveyHos.Theme/lib/datatables/jquery.dataTables.min.js", "~/SurveyHos.Theme/lib/datatables/jquery.dataTables.js")
              .SetVersion("1.11.0");

            _manifest
                   .DefineScript("surveyhos-vendor-datatables-bs4")
                   .SetUrl("~/SurveyHos.Theme/lib/datatables-bs4/js/dataTables.bootstrap4.min.js", "~/SurveyHos.Theme/lib/datatables-bs4/js/dataTables.bootstrap4.js")
                   .SetVersion("1.10");

            _manifest
                     .DefineScript("surveyhos-vendor-moment")
                     .SetUrl("~/SurveyHos.Theme/lib/moment/js/moment.min.js", "~/SurveyHos.Theme/lib/moment/js/moment.min.js")
                     .SetVersion("2.18.1");

            _manifest
                       .DefineStyle("surveyhos-vendor-datatables-bs4")
                       .SetUrl("~/SurveyHos.Theme/lib/datatables-bs4/css/dataTables.bootstrap4.min.css", "~/SurveyHos.Theme/lib/datatables-bs4/css/dataTables.bootstrap4.css")
                       .SetVersion("1.10");

            //_manifest
            //          .DefineStyle("surveyhos-toastr")
            //          .SetUrl("~/SurveyHos.Theme/lib/toastr/toastr.min.css", "~/SurveyHos.Theme/lib/toastr/toastr.css")
            //          .SetVersion("1.0.0");
            //_manifest
            //        .DefineScript("surveyhos-toastr")
            //        .SetUrl("~/SurveyHos.Theme/lib/toastr/toastr.min.js", "~/SurveyHos.Theme/lib/toastr/toastr.js")
            //        .SetVersion("1.0.0");
            _manifest
                   .DefineStyle("surveyhos-sweetalert2")
                   .SetUrl("~/SurveyHos.Theme/lib/sweetalert2/sweetalert2.min.css", "~/SurveyHos.Theme/lib/sweetalert2/sweetalert2.min.css")
                   .SetVersion("1.0.0");
            _manifest
                    .DefineScript("surveyhos-sweetalert2")
                    .SetUrl("~/SurveyHos.Theme/lib/sweetalert2/sweetalert2.min.js", "~/SurveyHos.Theme/lib/sweetalert2/sweetalert2.min.js")
                   .SetVersion("1.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
