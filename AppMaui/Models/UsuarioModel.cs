using AppMaui.Generic;
using AppMaui.Modelos;

namespace AppMaui.Models
{
    public class UsuarioModel : BaseBinding
    {
        private UsuarioCLS _oUsuarioCLS;

        public UsuarioCLS oUsuarioCLS
        {
            get { return _oUsuarioCLS; }
            set { SetValue(ref _oUsuarioCLS, value); }
        }
    }
}
