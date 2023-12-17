using AppMaui.Generic;
using AppMaui.Modelos;

namespace AppMaui.Models
{
    public class OpcionesPrinciplaModel : BaseBinding
    {
        private List<OpcionesPrincipalCLS> _listopcionesprincipal;

        public List<OpcionesPrincipalCLS> listopcionesprincipal
        {
            get { return _listopcionesprincipal; }
            set { SetValue(ref _listopcionesprincipal, value); }
        }
    }
}
