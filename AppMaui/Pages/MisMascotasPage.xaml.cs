

using AppMaui.Modelos;
using AppMaui.Models;
using MyWeb.Clases;

namespace AppMaui.Pages;

public partial class MisMascotasPage : ContentPage
{

    private string urlBase;
    private string urlUsuario;
    private string token;

 public MascotaModel oMascotaModel { get; set; }

    public MisMascotasPage()
	{
		InitializeComponent();
        urlBase = App.Current.Resources["urlBase"].ToString();
        urlUsuario = App.Current.Resources["urlMascota"].ToString();
        token = App.Current.Resources["Token"].ToString();
        oMascotaModel = new MascotaModel();
        oMascotaModel.listmascota = new List<MascotaCLS>();
        BindingContext = this;
        recuperarMascota();
	}

    private async void recuperarMascota()
    {
        int idusuario = Preferences.Get("iidusuario", 0);
        List<MascotaCLS> lista = await ClienteHttp.GetAll<MascotaCLS>(urlBase, urlUsuario + idusuario, token);
        oMascotaModel.listmascota = lista;
    }

    private void lstMascota_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        MascotaCLS obj = (MascotaCLS)e.Item;
        App.Navigate.PushAsync(new FormMascotaPage("Editar Mascota", obj.iidmascota));
    }

    private void btnNuevaMascota_Clicked(object sender, EventArgs e)
    {
        App.Navigate.PushAsync(new FormMascotaPage("Agregar Mascota", 0));
    }
}