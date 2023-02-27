using Microsoft.AspNetCore.Mvc;

namespace DroneManager.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        protected ApiControllerBase()
        {
        }
    }
}
