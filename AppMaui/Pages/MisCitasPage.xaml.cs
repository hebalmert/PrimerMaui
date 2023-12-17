using AppMaui.Modelos;
using AppMaui.Models;
using MyWeb.Clases;

namespace AppMaui.Pages;

public partial class MisCitasPage : ContentPage
{
    public CitaModel oCitaModel { get; set; } = new CitaModel();

    private string urlBase;
    private string urlMascota;
    private string urlCita;
    private string token;

    public MisCitasPage()
	{
        InitializeComponent();
        urlBase = App.Current.Resources["urlBase"].ToString();
        urlMascota = App.Current.Resources["urlMascota"].ToString();
        urlCita = App.Current.Resources["urlCita"].ToString();
        token = App.Current.Resources["Token"].ToString();
        oCitaModel.listacita = new List<CitaCLS>();
        BindingContext = this;
        listarCitasUsuario();
    }

    private async void listarCitasUsuario()
    {
        int idusuario = Preferences.Get("iidusuario", 0);
        List<CitaCLS> listacita = await ClienteHttp.GetAll<CitaCLS>(urlBase, urlCita + idusuario, token);
        oCitaModel.listacita = listacita;
    }

    private void btnNuevaCita_Clicked(object sender, EventArgs e)
    {
        App.Navigate.PushAsync(new FormCitaPage());
    }

    private void tabOpcionPrincipal_Tapped(object sender, TappedEventArgs e)
    {

    }
}