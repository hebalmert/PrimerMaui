using AppMaui.Generic;
using AppMaui.Modelos;

namespace AppMaui.Models
{
    public class PersonaModel : BaseBinding
    {
        private PersonaCLS _oPersonaCLS;

        public PersonaCLS oPersonaCLS
        {
            get { return _oPersonaCLS; }
            set { SetValue(ref _oPersonaCLS, value); }
        }


        private byte[] _imagen;

        public byte[] imagen
        {
            get { return _imagen; }
            set { SetValue(ref _imagen, value); }
        }
    }
}
