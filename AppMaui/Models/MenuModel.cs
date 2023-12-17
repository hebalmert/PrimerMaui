using AppMaui.Generic;
using AppMaui.Modelos;

namespace AppMaui.Models
{
    public class MenuModel : BaseBinding
    {
        private List<MenuCLS> _listaMenu;

        public List<MenuCLS> ListaMenu
        {
            get { return _listaMenu; }
            set { SetValue(ref _listaMenu, value); }
        }
    }
}
