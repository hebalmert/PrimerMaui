using Capa_Entitdad;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Filters;
using MyWebApi.Generic;
using MyWebApi.Models;
using System.Transactions;

namespace MyWebApi.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    [ServiceFilter(typeof(Seguridad))]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        public List<UsuarioCLS> ListarUsuario()
        {
            List<UsuarioCLS> lista = new List<UsuarioCLS>();
            try
            {
                using (DataContext context = new DataContext())
                {
                    lista = (from usuario in context.Usuarios
                             join persona in context.Personas on usuario.Iidpersona equals persona.Iidpersona
                             join tipousuario in context.TipoUsuarios on usuario.Iidtipousuario equals tipousuario.Iidtipousuario
                             where usuario.Bhabilitado == 1
                             select new UsuarioCLS
                             {
                                 iidusuario = usuario.Iidusuario,
                                 nombreusuario = usuario.Nombreusuario!.ToLower(),
                                 nombrepersona = $"{persona.Nombre} {persona.Appaterno} {persona.Apmaterno}",
                                 iidtipousuario = (int)usuario.Iidtipousuario!,
                                 fotopersona = persona.Varchivo == null ? "" : $"data:image/{System.IO.Path.GetExtension(persona.Vnombrearchivo)!.Substring(1)};base64,{Convert.ToBase64String(persona.Varchivo)}",
                                 nombretipousuario = tipousuario.Nombre
                             }).ToList();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        [HttpGet("{id}")]
        public UsuarioCLS recuperarUsuario(int id)
        {
            UsuarioCLS oUsuarioCLS = new UsuarioCLS();
            try
            {
                using (DataContext context = new DataContext())
                {
                    oUsuarioCLS = (from usuario in context.Usuarios
                                   where usuario.Iidusuario == id
                                   select new UsuarioCLS
                                   {
                                       iidusuario = usuario.Iidusuario,
                                       nombreusuario = usuario.Nombreusuario,
                                       iidtipousuario = (int)usuario.Iidtipousuario!
                                   }).First();
                    return oUsuarioCLS;

                }
            }
            catch (Exception)
            {

                return oUsuarioCLS;
            }
        }

        [HttpGet("recuperarPersonaPorIdUsuario/{id}")]
        public PersonaCLS recuperarPersonaProIdUsuario(int id)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();
            try
            {
                using (DataContext context = new DataContext())
                {
                    int idpersoan = (int)context.Usuarios.Where(x => x.Iidusuario == id).First().Iidpersona!;
                    oPersonaCLS = (from persona in context.Personas
                                   join sexo in context.Sexos on persona.Iidsexo equals sexo.Iidsexo
                                   join usuario in context.Usuarios on persona.Iidpersona equals usuario.Iidpersona
                                   join tipousuario in context.TipoUsuarios on usuario.Iidtipousuario equals tipousuario.Iidtipousuario
                                   where persona.Iidpersona == idpersoan
                                   select new PersonaCLS
                                   {
                                       iidpersona = persona.Iidpersona,
                                       nombre = persona.Nombre,
                                       appaterno = persona.Appaterno,
                                       apmaterno = persona.Apmaterno,
                                       nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                       correo = persona.Correo,
                                       fechanacimiento = (DateTime)persona.Fechanacimiento!,
                                       fechanacimientocadena = persona.Fechanacimiento == null ? "" : persona.Fechanacimiento.Value.ToString("dd/MM/yyyy"),
                                       iidSexo = (int)persona.Iidsexo!,
                                       archivo = persona.Varchivo,
                                       telefono = persona.Telefono,
                                       nombresexo = sexo.Nombre,
                                       nombreusuario = usuario.Nombreusuario,
                                       nombretipousuario = tipousuario.Nombre
                                   }).First();
                    return oPersonaCLS;
                }
            }
            catch (Exception)
            {

                return oPersonaCLS;
            }
        }

        [HttpPost]
        public List<UsuarioCLS> buscarUsuario([FromBody] UsuarioCLS oUsuarioCLS)
        {
            List<UsuarioCLS> lista = ListarUsuario();

            if (oUsuarioCLS.nombreusuario != null)
            {
                lista = lista.Where(x => x.nombreusuario!.Contains(oUsuarioCLS.nombreusuario)).ToList();
            }
            if (oUsuarioCLS.iidtipousuario != 0)
            {
                lista = lista.Where(x => x.iidtipousuario == oUsuarioCLS.iidtipousuario).ToList();
            }
            return lista;
        }

        [HttpDelete("{id}")]
        public int eliminarUsuario(int id)
        {
            int rpta = 0;
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    using (DataContext context = new DataContext())
                    {
                        Usuario oUsuario = context.Usuarios.Where(x => x.Iidusuario == id).First();
                        oUsuario.Bhabilitado = 0;
                        context.Update(oUsuario);
                        context.SaveChanges();

                        Persona oPersona = context.Personas.Where(x => x.Iidpersona == oUsuario.Iidpersona).First();
                        oPersona.Btieneusuario = 0;
                        context.Update(oPersona);
                        context.SaveChanges();

                        transaction.Complete();
                        rpta = 1;

                        return rpta;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("cambiarClaveUsuario")]
        public int cambiarClaveUsuario([FromBody] UsuarioCLS oUsuarioCLS)
        {
            int rpta = 0;
            try
            {
                using (DataContext context = new DataContext())
                {
                    //si la contra es correcta
                    string claveActual = oUsuarioCLS.contra!;
                    string claveCifradaActual = Utils.CifrarCadena(claveActual);
                    int Nveces = context.Usuarios.Where(x => x.Iidusuario == oUsuarioCLS.iidusuario && x.Contra == claveCifradaActual).Count();
                    if (Nveces == 0)
                    {
                        return -1;
                    }

                    string clave = oUsuarioCLS.contranueva!;
                    string claveCifrada = Utils.CifrarCadena(clave);
                    Usuario oUsuario = context.Usuarios.Where(x => x.Iidusuario == oUsuarioCLS.iidusuario).First();
                    oUsuario.Contra = claveCifrada;
                    context.Update(oUsuario);
                    context.SaveChanges();
                }
                rpta = 1;
                return rpta;
            }
            catch (Exception)
            {

                return rpta;
            }
        }


        [HttpPost("guardarDatos")]
        public int guardarDatos([FromBody] UsuarioCLS oUsuarioCLS)
        {
            int rpta = 0;
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    using (DataContext context = new DataContext())
                    {
                        if (oUsuarioCLS.iidusuario == 0)
                        {
                            Usuario oUsuario = new Usuario();
                            oUsuario.Nombreusuario = oUsuarioCLS.nombreusuario;
                            oUsuario.Iidtipousuario = oUsuarioCLS.iidtipousuario;
                            oUsuario.Iidpersona = oUsuarioCLS.iidpersona;
                            oUsuario.Contra = Utils.CifrarCadena("12345678");
                            oUsuario.Bhabilitado = 1;
                            context.Usuarios.Add(oUsuario);
                            context.SaveChanges();

                            Persona opersona = context.Personas.Where(x => x.Iidpersona == oUsuarioCLS.iidpersona).FirstOrDefault()!;
                            opersona.Btieneusuario = 1;
                            context.Update(opersona);
                            context.SaveChanges();

                            transaction.Complete();

                            rpta = 1;
                        }
                        else
                        {
                            Usuario oUsuario = context.Usuarios.Where(x => x.Iidusuario == oUsuarioCLS.iidusuario).First();
                            oUsuario.Nombreusuario = oUsuarioCLS.nombreusuario;
                            oUsuario.Iidtipousuario = oUsuarioCLS.iidtipousuario;
                            context.Usuarios.Update(oUsuario);
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

                return 0;
            }
        }


    }
}
