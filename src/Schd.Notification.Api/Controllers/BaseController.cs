using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Schd.Common.Response;
using ApiResponseType = Schd.Common.Response.ApiResponseType;

namespace Schd.Notification.Controllers
{
    [ApiController]
    [Produces(ApiResponseType.JsonResponse)]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
    }
}
