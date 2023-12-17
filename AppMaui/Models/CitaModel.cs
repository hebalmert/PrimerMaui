using AppMaui.Generic;
using AppMaui.Modelos;

namespace AppMaui.Models
{
    public class CitaModel : BaseBinding
    {
        private CitaCLS _oCitaCLS;

        public CitaCLS oCitaCLS
        {
            get { return _oCitaCLS; }
            set { SetValue(ref _oCitaCLS, value); }
        }

        private TipoMascotaCLS _itemTipoMascota;
        public TipoMascotaCLS itemTipoMascota
        {
            get { return _itemTipoMascota; }
            set { SetValue(ref _itemTipoMascota, value); }
        }

        private List<TipoMascotaCLS> _listatipomascota;
        public List<TipoMascotaCLS> listatipomascota
        {
            get { return _listatipomascota; }
            set { SetValue(ref _listatipomascota, value); }
        }

        private MascotaCLS _itemMascota;
        public MascotaCLS itemMascota
        {
            get { return _itemMascota; }
            set { SetValue(ref _itemMascota, value); }
        }

        private List<MascotaCLS> _listamascota;
        public List<MascotaCLS> listamascota
        {
            get { return _listamascota; }
            set { SetValue(ref _listamascota, value); }
        }

        private SedeCLS _itemSede;
        public SedeCLS itemSede
        {
            get { return _itemSede; }
            set { SetValue(ref _itemSede, value); }
        }

        private List<SedeCLS> _listaSede;
        public List<SedeCLS> listaSede
        {
            get { return _listaSede; }
            set { SetValue(ref _listaSede, value); }
        }

        private List<CitaCLS> _listacita;
        public List<CitaCLS> listacita
        {
            get { return _listacita; }
            set { SetValue(ref _listacita, value); }
        }
    }
}
