using Capa_Entitdad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/Sede")]
    [ApiController]
    public class SedeController : ControllerBase
    {
        [HttpGet]
        public List<SedeCLS> ListarTipoUsuario()
        {
            try
            {
                List<SedeCLS> Lista = new List<SedeCLS>();
                using (DataContext context = new DataContext())
                {
                    Lista = (from sede in context.Sedes
                             where sede.Bhabilitado == 1
                             select new SedeCLS
                             {
                                 iidsede = sede.Iidsede,
                                 nombresede = sede.Vnombre
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
