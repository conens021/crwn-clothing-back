using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using DroneDropshipping.BLL.Exceptions;

namespace CrwnClothing.Presentation.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error-dev")]
        public ActionResult GetDevError([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            Exception? error = context?.Error;


            if (error is BusinessException)
            {
                BusinessException bussinesException = (BusinessException)error;
                return Problem(
                                  detail: bussinesException.StackTrace,
                                  title: bussinesException.Message,
                                  statusCode: bussinesException.StatusCode
                               );
            }

            return Problem(
                detail: error?.InnerException?.Message ?? "",
                title: context?.Error.Message ?? "Unknow error");
        }
    }
}
