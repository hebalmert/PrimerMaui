using Capa_Entitdad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using System.Transactions;

namespace MyWebApi.Controllers
{
    [Route("api/Cita")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        [HttpGet("{idusuario}")]
        public List<CitaCLS> ListarCitas(int idusuario)
        {
            try
            {
                List<CitaCLS> Lista = new List<CitaCLS>();
                using (DataContext context = new DataContext())
                {
                    Lista = (from cita in context.Cita
                             join mascota in context.Mascota on cita.Iidmascota equals mascota.Iidmascota
                             join tipomascota  in context.TipoMascota on mascota.Iidtipomascota equals tipomascota.Iidtipomascota
                             join usuario in context.Usuarios on cita.Iidusuario equals usuario.Iidusuario
                             join persona in context.Personas on usuario.Iidpersona equals persona.Iidpersona
                             join estadocita in context.EstadoCita on cita.Iidestadocita equals estadocita.Iidestado
                             where cita.Bhabilitado == 1 && cita.Iidusuario == idusuario
                             select new CitaCLS
                             {
                                 iidcita = cita.Iidcita,
                                 descripcionenfermedad = cita.Vdescripcion!,
                                 nombremascota = mascota.Nombre!,
                                 fotomascota = mascota.Archivo!,
                                 fechaenfermedadcadena = cita.Dfechaenfermo == null ? "" : cita.Dfechaenfermo!.Value.ToShortDateString(),
                                 nombretipomascota = tipomascota.Nombre!,
                                 nombreusuariomascota = persona.Nombre! + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 nombreestadocita = estadocita.Vnombre!
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
        public int guardarCita([FromBody] CitaCLS oCitaCLS)
        {
            int rpta = 0;
            try
            {
                int iidcita = oCitaCLS.iidcita;
                using (TransactionScope transaction = new TransactionScope())
                {
                    using (DataContext context = new DataContext())
                    {
                        if (iidcita == 0)
                        {
                            Citum oCitum = new Citum();
                            oCitum.Iidusuario = oCitaCLS.iidusuario;
                            oCitum.Iidtipomascota = oCitaCLS.iidtipomascota;
                            oCitum.Iidmascota = oCitaCLS.iidmascota;
                            oCitum.Vdescripcion = oCitaCLS.descripcionenfermedad;
                            oCitum.Dfechaenfermo = oCitaCLS.fechaenfermedad;
                            oCitum.Dfechainicio = DateTime.Now;
                            oCitum.Iidsede = oCitaCLS.iidsede;
                            oCitum.Iidestadocita = oCitaCLS.iidestadocita;
                            oCitum.Bhabilitado = 1;
                            context.Cita.Add(oCitum);
                            context.SaveChanges();
                            oCitaCLS.iidcita = oCitum.Iidcita;
                            int reg = insertarHistoriaCita(oCitaCLS, "Se Registro la Cita", context);
                            if (reg == 0) return 0;

                            transaction.Complete();

                            rpta = 1;
                        }
                        else
                        {
                            Citum oCitum = context.Cita.Where(x => x.Iidcita == iidcita).First();
                            oCitum.Iidtipomascota = oCitaCLS.iidtipomascota;
                            oCitum.Iidmascota = oCitaCLS.iidmascota;
                            oCitum.Vdescripcion = oCitaCLS.descripcionenfermedad;
                            oCitum.Dfechaenfermo = oCitaCLS.fechaenfermedad;
                            oCitum.Iidsede = oCitaCLS.iidsede;
                            context.Cita.Update(oCitum);
                            context.SaveChanges();

                            transaction.Complete();
                            rpta = 1;
                        }
                    }
                }

                return rpta;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        private int insertarHistoriaCita(CitaCLS oCitaCLS, string observacion, DataContext context)
        {
            int rpta = 0;
            try
            {
                HistorialCitum oHistorialCitum = new HistorialCitum();
                oHistorialCitum.Iidcita = oCitaCLS.iidcita;
                oHistorialCitum.Iidestado = oCitaCLS.iidestadocita;
                oHistorialCitum.Iidusuario = oCitaCLS.iidusuario;
                oHistorialCitum.Dfecha = DateTime.Now;
                oHistorialCitum.Vobservacion = observacion;
                context.HistorialCita.Add(oHistorialCitum);
                context.SaveChanges();

                rpta = 1;
                return rpta;
            }
            catch (Exception)
            {

                return rpta;
            }

        }
    }
}
