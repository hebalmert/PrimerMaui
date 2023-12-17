using Capa_Entitdad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/TipoMascota")]
    [ApiController]
    public class TipoMascotaController : ControllerBase
    {
        [HttpGet]
        public List<TipoMascotaCLS> ListarTipoMascota()
        {
            try
            {
                List<TipoMascotaCLS> Lista = new List<TipoMascotaCLS>();
                using (DataContext context = new DataContext())
                {
                    Lista = (from tipomascota in context.TipoMascota
                             where tipomascota.Bhabilitado == 1
                             select new TipoMascotaCLS
                             {
                                 iidtipomascota = tipomascota.Iidtipomascota,
                                 nombretipomascota = tipomascota.Nombre
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
