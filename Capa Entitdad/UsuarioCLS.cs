using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entitdad
{
    public class UsuarioCLS
    {
        public int iidusuario { get; set; } = 0;

        public string? nombreusuario { get; set; } = "";

        public string? contra { get; set; } = "";

        public string? contranueva { get; set; } = "";

        public string? contrarepitenueva { get; set; } = "";

        public int iidtipousuario { get; set; } = 0;

        public int iidpersona { get; set; } = 0;

        public string? nombrepersona { get; set; } = "";

        public string? nombretipousuario { get; set; } = "";

        public string? fotopersona { get; set; } = "";
    }
}
