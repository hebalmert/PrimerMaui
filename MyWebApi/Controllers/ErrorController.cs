using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public string ErrorApi() 
        {
            return "No tiene permiso para acceder a la API";
        }
    }
}
