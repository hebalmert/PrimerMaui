using AppMaui.Generic;
using AppMaui.Modelos;

namespace AppMaui.Models
{
    public class MascotaModel : BaseBinding
    {
        private byte[] _foto;

        public byte[] foto
        {
            get { return _foto; }
            set { SetValue(ref _foto, value); }
        }

        private string _itemSexo;
        public string itemSexo
        {
            get { return _itemSexo; }
            set { SetValue(ref _itemSexo, value); }
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


        private MascotaCLS _oMascotaCLS;
        public MascotaCLS oMascotaCLS
        {
            get { return _oMascotaCLS; }
            set { SetValue(ref _oMascotaCLS, value); }
        }

        private List<MascotaCLS> _listmascota;
        public List<MascotaCLS> listmascota
        {
            get { return _listmascota; }
            set { SetValue(ref _listmascota, value); }
        }
    }
}
