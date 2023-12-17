using AppMaui.Modelos;
using AppMaui.Models;
using MyWeb.Clases;

namespace AppMaui.Pages;

public partial class UsuarioPage : ContentPage
{
	public PersonaModel oPersonaModel { get; set; }

    private string urlBase;
    private string urlUsuario;
    private string token;

    public UsuarioPage()
	{
		InitializeComponent();
        urlBase = App.Current.Resources["urlBase"].ToString();
        urlUsuario = App.Current.Resources["urlUsuario"].ToString();
        token = App.Current.Resources["Token"].ToString();
        oPersonaModel = new PersonaModel();
        oPersonaModel.oPersonaCLS = new PersonaCLS();
        BindingContext = this;
        RecuperarUIsuario();
	}

    private async void RecuperarUIsuario()
    {
        int idusuario = Preferences.Get("iidusuario", 0);
        PersonaCLS oPersonaCLS = await ClienteHttp.Get<PersonaCLS>(urlBase, urlUsuario + "recuperarPersonaPorIdUsuario/" + idusuario , token);
        oPersonaModel.oPersonaCLS = oPersonaCLS;
        //oPersonaModel.imagen = oPersonaCLS.archivo == null ? "noimagen.jpg" : ImageSource.FromStream(() => new MemoryStream(oPersonaCLS.archivo));
    }
}