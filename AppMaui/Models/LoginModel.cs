using AppMaui.Generic;

namespace AppMaui.Models
{
    public class LoginModel : BaseBinding
    {
        private string _nombreusuario;
        private string _contra;
        private bool _IsCargando;


        public string NombreUsuario
        {
            get { return _nombreusuario; }
            set { SetValue(ref _nombreusuario, value); }
        }


        public string Contra
        {
            get { return _contra; }
            set { SetValue(ref _contra, value); }
        }

        public bool IsCargando
        {
            get { return _IsCargando; }
            set { SetValue(ref _IsCargando, value); }
        }

    }
}
