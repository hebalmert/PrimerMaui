using AppMaui.Modelos;
using AppMaui.Models;
using MyWeb.Clases;

namespace AppMaui.Pages;

public partial class FormCitaPage : ContentPage
{
    public CitaModel oCitaModel { get; set; } = new CitaModel();

    private string urlBase;
    private string urlMascota;
    private string urlTipoMascota;
    private string urlSede;
    private string urlCita;
    private string token;

    public FormCitaPage()
    {
        InitializeComponent();
        urlBase = App.Current.Resources["urlBase"].ToString();
        urlMascota = App.Current.Resources["urlMascota"].ToString();
        urlTipoMascota = App.Current.Resources["urlTipoMascota"].ToString();
        urlSede = App.Current.Resources["urlSede"].ToString();
        urlCita = App.Current.Resources["urlCita"].ToString();
        token = App.Current.Resources["Token"].ToString();
        oCitaModel.oCitaCLS = new CitaCLS();
        oCitaModel.listatipomascota = new List<TipoMascotaCLS>();
        oCitaModel.listamascota = new List<MascotaCLS>();
        oCitaModel.listaSede = new List<SedeCLS>();
        BindingContext = this;
        listarPicker();
    }

    private List<MascotaCLS> listaMascota;
    private async void listarPicker()
    {
        List<TipoMascotaCLS> listaTipoMascota = await ClienteHttp.GetAll<TipoMascotaCLS>(urlBase, urlTipoMascota, token);
        listaTipoMascota.Insert(0, new TipoMascotaCLS { iidtipomascota = 0, nombretipomascota = "--Seleccione Tipo--" });
        oCitaModel.listatipomascota = listaTipoMascota;
        oCitaModel.itemTipoMascota = oCitaModel.listatipomascota[0];

        int idusuario = Preferences.Get("iidusuario", 0);
        listaMascota = await ClienteHttp.GetAll<MascotaCLS>(urlBase, urlMascota + idusuario, token);
        oCitaModel.listamascota = new List<MascotaCLS>{new MascotaCLS
        {
            iidmascota = 0,
            nombremascota = "----Seleccione----"
        }};
        oCitaModel.itemMascota = oCitaModel.listamascota[0];
        List<SedeCLS> listasede = await ClienteHttp.GetAll<SedeCLS>(urlBase, urlSede, token);
        listasede.Insert(0, new SedeCLS
        {
            iidsede = 0,
            nombresede = "----Seleccione----"
        });
        oCitaModel.listaSede = listasede;
        oCitaModel.itemSede = oCitaModel.listaSede[0];
    }

    private void picketTipoMascota_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listaMascota != null)
        {
            string nombretipomascota = oCitaModel.itemTipoMascota.nombretipomascota;
            List<MascotaCLS> listaMascotaFiltrada = listaMascota.Where(x => x.nombretipomascota == nombretipomascota).ToList();
            listaMascotaFiltrada.Insert(0, new MascotaCLS
            {
                iidmascota = 0,
                nombremascota = "----Seleccione----"
            });
            oCitaModel.listamascota = listaMascotaFiltrada;
            oCitaModel.itemMascota = oCitaModel.listamascota[0];
        }
    }

    private async void btnAceptarCita_Clicked(object sender, EventArgs e)
    {
        int idusuario = Preferences.Get("iidusuario", 0);
        oCitaModel.oCitaCLS.iidtipomascota = oCitaModel.itemTipoMascota.iidtipomascota;
        oCitaModel.oCitaCLS.iidmascota = oCitaModel.itemMascota.iidmascota;
        oCitaModel.oCitaCLS.iidsede = oCitaModel.itemSede.iidsede;
        oCitaModel.oCitaCLS.iidestadocita = 1;
        oCitaModel.oCitaCLS.iidusuario = idusuario;
        int rpta = await ClienteHttp.Post<CitaCLS>(urlBase, urlCita, oCitaModel.oCitaCLS, token);
        if (rpta == 1)
        {
            await Application.Current.MainPage.DisplayAlert("Exito", "Se Creado la Cita", "Aceptar");
            Application.Current.MainPage = new PrincipalPage();

        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un Error", "Aceptar");
        }

    }

    private void btnregresar_Clicked(object sender, EventArgs e)
    {

    }
}