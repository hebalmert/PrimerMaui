using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using Capa_Entitdad;
using MyWebApi.Filters;
namespace MyWebApi.Controllers
{
    [Route("api/Persona")]
    [ApiController]
    [ServiceFilter(typeof(Seguridad))]
    public class PersonaController : ControllerBase
    {
        [HttpGet]
        public List<PersonaCLS> ListarPersonas()
        {
            try
            {
                List<PersonaCLS> Lista = new List<PersonaCLS>();
                using (DataContext context = new DataContext())
                {
                    Lista = (from persona in context.Personas
                             where persona.Bhabilitado == 1
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = $"{persona.Nombre} {persona.Appaterno} {persona.Apmaterno}",
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" : persona.Fechanacimiento.Value.ToString("dd/MM/yyyy"),
                                 fotocadena = persona.Varchivo == null ? "" : $"data:image/{System.IO.Path.GetExtension(persona.Vnombrearchivo)!.Substring(1)};base64,{Convert.ToBase64String(persona.Varchivo)}"
                             }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("ListarPersonasSinUsuario")]
        public List<PersonaCLS> ListarPersonasSinUsuario()
        {
            try
            {
                List<PersonaCLS> Lista = new List<PersonaCLS>();
                using (DataContext context = new DataContext())
                {
                    Lista = (from persona in context.Personas
                             where persona.Bhabilitado == 1 && persona.Btieneusuario == 0
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = $"{persona.Nombre} {persona.Appaterno} {persona.Apmaterno}"
                             }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpGet("recuperarPersonaPorId/{id}")]
        public PersonaCLS RecuperarPeronaPorId(int id)
        {
            PersonaCLS Opersona = new PersonaCLS();
            try
            {
                using (DataContext context = new DataContext())
                {

                    Opersona = (from persona in context.Personas
                                where persona.Iidpersona == id
                                select new PersonaCLS
                                {
                                    iidpersona = persona.Iidpersona,
                                    nombre = persona.Nombre,
                                    appaterno = persona.Appaterno,
                                    apmaterno = persona.Apmaterno,
                                    correo = persona.Correo,
                                    fechanacimiento = (DateTime)persona.Fechanacimiento!,
                                    fechanacimientocadena = persona.Fechanacimiento == null ? "" : persona.Fechanacimiento.Value.ToString("yyyy-MM-dd"),
                                    fotocadena = persona.Varchivo == null ? "" : $"data:image/{System.IO.Path.GetExtension(persona.Vnombrearchivo)!.Substring(1)};base64,{Convert.ToBase64String(persona.Varchivo)}"
                                }).First();

                    return Opersona;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpGet("{nombrecompleto}")]
        public List<PersonaCLS> BuscarPersonas(string nombrecompleto)
        {
            List<PersonaCLS> Lista = new List<PersonaCLS>();
            try
            {
                using (DataContext context = new DataContext())
                {
                    Lista = (from persona in context.Personas
                             where persona.Bhabilitado == 1 &&
                             (persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno).Contains(nombrecompleto)
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = $"{persona.Nombre} {persona.Appaterno} {persona.Apmaterno}",
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" : persona.Fechanacimiento.Value.ToString("dd/MM/yyyy"),
                                 fotocadena = persona.Varchivo == null ? "" : $"data:image/{System.IO.Path.GetExtension(persona.Vnombrearchivo)!.Substring(1)};base64,{Convert.ToBase64String(persona.Varchivo)}"
                             }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {

                return Lista;
            }
        }

        [HttpDelete("{id}")]
        public int eliminarPersona(int id)
        {
            try
            {
                //No vamos a liminar el registro solo a cambiar su Habilitado a 0 para que no lo liste
                //asi conservar la data de la base de datos para seguir trabajando con ella

                // si es error es Cero y si es Exitoso es 1
                int rpta = 0;
                using (DataContext context = new DataContext())
                {
                    Persona oPersona = context.Personas.First(x => x.Iidpersona == id);
                    oPersona.Bhabilitado = 0;
                    context.Update(oPersona);
                    context.SaveChanges();
                    rpta = 1;
                    return rpta;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        [HttpPost]
        public int guardarPersona([FromBody] PersonaCLS oPersonaCLS)
        {
            // si es error es Cero y si es Exitoso es 1
            int rpta = 0;
            try
            {
                int id = oPersonaCLS.iidpersona;
                using (DataContext context = new DataContext())
                {
                    //Si el Id es Cero 0, es porque es un registro nuevo
                    if (id == 0)
                    {
                        Persona oPersona = new Persona();
                        oPersona.Nombre = oPersonaCLS.nombre;
                        oPersona.Appaterno = oPersonaCLS.appaterno;
                        oPersona.Apmaterno = oPersonaCLS.apmaterno;
                        oPersona.Correo = oPersonaCLS.correo;
                        oPersona.Fechanacimiento = DateTime.Parse(oPersonaCLS.fechanacimientocadena!);
                        oPersona.Btieneusuario = 0;
                        oPersona.Iidsexo = oPersonaCLS.iidSexo;
                        if (oPersonaCLS.nombrearchivo != "")
                        {
                            oPersona.Vnombrearchivo = oPersonaCLS.nombrearchivo;
                            oPersona.Varchivo = oPersonaCLS.archivo;
                        }
                        oPersona.Bhabilitado = 1;
                        context.Add(oPersona);
                        context.SaveChanges();
                        rpta = 1;
                    }
                    else
                    {
                        Persona oPersona = context.Personas.First(x => x.Iidpersona == id);
                        oPersona.Nombre = oPersonaCLS.nombre;
                        oPersona.Appaterno = oPersonaCLS.appaterno;
                        oPersona.Apmaterno = oPersonaCLS.apmaterno;
                        oPersona.Telefono = oPersonaCLS.telefono;
                        oPersona.Correo = oPersonaCLS.correo;
                        oPersona.Fechanacimiento = DateTime.Parse(oPersonaCLS.fechanacimientocadena!);
                        oPersona.Iidsexo = oPersonaCLS.iidSexo;
                        if (oPersonaCLS.nombrearchivo != "")
                        {
                            oPersona.Vnombrearchivo = oPersonaCLS.nombrearchivo;
                            oPersona.Varchivo = oPersonaCLS.archivo;
                        }
                        context.Update(oPersona);
                        context.SaveChanges();
                        rpta = 1;
                    }
                }
                
                return rpta;
            }
            catch (Exception)
            {

                return rpta = 0;
            }
        }

    }
}
