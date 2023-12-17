using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWebApi.Filters
{
    public class Seguridad : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }


        //Este metodo se ejecuta Antes de entrar a cada Metodo dentro de los controladores
        public void OnActionExecuting(ActionExecutingContext context)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            //lee el valor del token en el appsetting
            string token = root.GetValue<string>("token")!;

            //Verificamos en envio del token del cliente que viene por los headers
            string TokenCliente = context.HttpContext.Request.Headers["token"]!;

            //verificamos token cliente con el del sistema
            if (TokenCliente == null || TokenCliente != token)
            {
                context.Result = new RedirectResult("/api/Error");

            }

        }
    }
}
