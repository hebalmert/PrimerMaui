using Capa_Entitdad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/TipoUsuarios")]
    [ApiController]
    public class TipoUsuariosController : ControllerBase
    {

        [HttpGet]
        public List<TipoUsuarioCLS> ListarTipoUsuario()
        {
            try
            {
                List<TipoUsuarioCLS> Lista = new List<TipoUsuarioCLS>();
                using (DataContext context = new DataContext())
                {
                    Lista = (from tipousuario in context.TipoUsuarios
                             where tipousuario.Bhabilitado == 1
                             select new TipoUsuarioCLS
                             {
                                 iidtipousuario = tipousuario.Iidtipousuario,
                                 nombretipousuario = tipousuario.Nombre
                             }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
