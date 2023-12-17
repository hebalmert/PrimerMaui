using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Generic;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("{nombreusuario}/{contra}")]
        public int login(string nombreusuario, string contra)
        {
            int rpta = 0;
            try
            {
                using (DataContext context = new DataContext())
                {
                    string contraCifra = Utils.CifrarCadena(contra);
                    var lista = context.Usuarios.Where(x => x.Nombreusuario == nombreusuario && x.Contra == contraCifra);
                    int cantidad = lista.Count();
                    if (cantidad == 0)
                    {
                        return rpta;
                    }
                    else
                    {
                        return lista.First().Iidusuario;
                    }
                }
            }
            catch (Exception)
            {

                return rpta;
            }
        }
    }
}
