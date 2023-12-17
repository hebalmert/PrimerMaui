using Capa_Entitdad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/Mascota")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        [HttpGet("recuperarMascota/{id}")]
        public MascotaCLS recuperarMascota(int id)
        {
            MascotaCLS oMascotaCLS;
            try
            {
                using (DataContext context = new DataContext())
                {
                    oMascotaCLS = (from mascota in context.Mascota
                                   where mascota.Iidmascota == id
                                   select new MascotaCLS
                                   {
                                       iidmascota = mascota.Iidmascota,
                                       iidusuariopropietario = (int)mascota.Iidusuariopropietario!,
                                       nombremascota = mascota.Nombre,
                                       iidtipomascota = (int)mascota.Iidtipomascota!,
                                       fechanacimiento = mascota.Fechanacimiento,
                                       ancho = mascota.Ancho,
                                       alto = mascota.Altura,
                                       iidsexo = (int)mascota.Iidsexo!,
                                       vobservacion = mascota.Vobservacion,
                                       foto = mascota.Archivo,
                                       nombrefoto = mascota.Vnombrearchivo
                                   }).First();

                    return oMascotaCLS;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("{id}")]
        public List<MascotaCLS> ListarMascota(int id) //id del usuario
        {
            try
            {
                List<MascotaCLS> Lista = new List<MascotaCLS>();
                using (DataContext context = new DataContext())
                {
                    Lista = (from mascota in context.Mascota
                             join tipomascota in context.TipoMascota on mascota.Iidtipomascota equals tipomascota.Iidtipomascota
                             where mascota.Bhabilitado == 1 && mascota.Iidusuariopropietario == id
                             select new MascotaCLS
                             {
                                 iidmascota = mascota.Iidmascota,
                                 nombremascota = mascota.Nombre,
                                 foto = mascota.Archivo,
                                 nombrefoto = mascota.Vnombrearchivo,
                                 nombresexo = mascota.Iidsexo == 1 ? "Macho" : "Hembra",
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

        [HttpPost]
        public int guardarMascota([FromBody] MascotaCLS oMascotaCLS)
        {
            int rpta = 0;
            try
            {
                int idmascota = oMascotaCLS.iidmascota;
                using (DataContext context = new DataContext())
                {
                    if (idmascota == 0)
                    {
                        Mascotum oMascotum = new Mascotum();
                        oMascotum.Iidusuariopropietario = oMascotaCLS.iidusuariopropietario;
                        oMascotum.Nombre = oMascotaCLS.nombremascota;
                        oMascotum.Iidtipomascota = oMascotaCLS.iidtipomascota;
                        oMascotum.Fechanacimiento = (DateTime)oMascotaCLS.fechanacimiento!;
                        oMascotum.Ancho = oMascotaCLS.ancho;
                        oMascotum.Altura = oMascotaCLS.alto;
                        oMascotum.Iidsexo = oMascotaCLS.iidsexo;
                        oMascotum.Vobservacion = oMascotaCLS.vobservacion;
                        if (oMascotaCLS.nombrefoto != "")
                        {
                            oMascotum.Vnombrearchivo = oMascotaCLS.nombrefoto;
                            oMascotum.Archivo = oMascotaCLS.foto;
                        }
                        oMascotum.Bhabilitado = 1;
                        context.Add(oMascotum);
                        context.SaveChanges();
                    }
                    else
                    { 
                        Mascotum oMascotum = context.Mascota.Where(c=> c.Iidmascota == idmascota).First();
                        oMascotum.Iidusuariopropietario = oMascotaCLS.iidusuariopropietario;
                        oMascotum.Nombre = oMascotaCLS.nombremascota;
                        oMascotum.Iidtipomascota = oMascotaCLS.iidtipomascota;
                        oMascotum.Fechanacimiento = (DateTime)oMascotaCLS.fechanacimiento!;
                        oMascotum.Ancho = oMascotaCLS.ancho;
                        oMascotum.Altura = oMascotaCLS.alto;
                        oMascotum.Iidsexo = oMascotaCLS.iidsexo;
                        oMascotum.Vobservacion = oMascotaCLS.vobservacion;
                        if (oMascotaCLS.nombrefoto != "")
                        {
                            oMascotum.Vnombrearchivo = oMascotaCLS.nombrefoto;
                            oMascotum.Archivo = oMascotaCLS.foto;
                        }
                        context.Update(oMascotum);
                        context.SaveChanges();
                    }
                }

                return rpta = 1;
            }
            catch (Exception)
            {

                rpta = 0;
                return rpta;
            }
        }
    }
}
