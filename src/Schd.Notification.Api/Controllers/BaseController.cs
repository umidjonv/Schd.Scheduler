using Microsoft.AspNetCore.Mvc;
using ApiResponseType = Schd.Common.Response.ApiResponseType;

namespace Schd.Notification.Api.Controllers
{
    [ApiController]
    [Produces(ApiResponseType.JsonResponse)]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
    }
}
