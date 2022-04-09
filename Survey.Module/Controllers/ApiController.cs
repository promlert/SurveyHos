using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using OrchardCore.Contents;
using Survey.Module.Indexes;
using Survey.Module.Models;
using System.Threading.Tasks;
using YesSql;
using ISession = YesSql.ISession;

namespace Survey.Module.Controllers
{
    [Route("api/survey/[action]")]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly ISession _session;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IContentManager _contentManager;
        private readonly ILogger _logger;
        public ApiController(ISession session,
                IHttpContextAccessor httpContextAccessor, ILogger<ApiController> logger, IAuthorizationService authorizationService, IContentManager contentManager)
        {
            _session = session;
            _httpContextAccessor = httpContextAccessor;
            _contentManager = contentManager;
            _authorizationService = authorizationService;
            _logger = logger;
        }
        [HttpPost]
        [Authorize]
        public ActionResult PostSurvey(SurveyModel model)
        {
            var ip = HttpContext.Connection.RemoteIpAddress;
            //try
            //{
            if (model.User != null)
            {
                _session.Save(new SurveyModel { CreateDate = System.DateTime.Now, Fair = model.Fair, Good = model.Good, Unsatisfy = model.Unsatisfy, Station = model.Station, User = model.User });
            }
            return Json(new { status = true, result = "Send Success!" });
            //}
            //catch (System.Exception ex)
            //{

            //    return Json(new { status = false, result = ex.Message, model });
            //}

        }
      
        public async Task<IActionResult> GetAuthorizedById(string id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.SurveyAPIAccess))
            {
                return this.ChallengeOrForbid("Api");
            }

            var contentItem = await _contentManager.GetAsync(id);

            if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ViewContent, contentItem))
            {
                return this.ChallengeOrForbid("Api");
            }

            if (contentItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(contentItem);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddContent(ContentItem contentItem)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.SurveyAPIAccess))
            {
                return this.ChallengeOrForbid("Api");
            }

            await _contentManager.CreateAsync(contentItem);

            return new ObjectResult(contentItem);
        }
        [HttpGet]
        public string IpClient()
        {
            var ip = HttpContext.Connection.RemoteIpAddress;
            return ip.ToString();
        }
    }
}
