using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schd.Common.Response;

namespace Schd.Notification.Controllers
{
    [ApiController]
    [Produces(ApiResponseType.JsonResponse)]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
    }
}
