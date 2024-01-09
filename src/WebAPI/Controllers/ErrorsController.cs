using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]

        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<ExceptionHandlerFeature>()?.Error;

            return Problem();
        }
    }
}
