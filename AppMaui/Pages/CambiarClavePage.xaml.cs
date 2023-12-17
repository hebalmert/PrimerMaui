using AppMaui.Modelos;
using AppMaui.Models;
using MyWeb.Clases;

namespace AppMaui.Pages;

public partial class CambiarClavePage : ContentPage
{
    private string urlBase;
    private string urlUsuario;
    private string token;

    public UsuarioModel oUsuarioModel { get; set; }

	public CambiarClavePage()
	{
		InitializeComponent();
        urlBase = App.Current.Resources["urlBase"].ToString();
        urlUsuario = App.Current.Resources["urlUsuario"].ToString();
        token = App.Current.Resources["Token"].ToString();
        oUsuarioModel = new UsuarioModel();
        oUsuarioModel.oUsuarioCLS = new UsuarioCLS();
        BindingContext = this;
    }

    private async void btnCambiarClave_Clicked(object sender, EventArgs e)
    {
        int idusuario = Preferences.Get("iidusuario", 0);
        oUsuarioModel.oUsuarioCLS.iidusuario = idusuario;
        if (oUsuarioModel.oUsuarioCLS.contranueva != oUsuarioModel.oUsuarioCLS.contrarepitenueva)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Contrasenas no Coinciden", "Cancelar");
            return;
        }
        int rpta = await ClienteHttp.Post<UsuarioCLS>(urlBase, urlUsuario + "cambiarClaveUsuario", oUsuarioModel.oUsuarioCLS, token);
        if (rpta == 1)
        {
            await Application.Current.MainPage.DisplayAlert("Exito", "Se Cambio la Clave con Exito", "Cancelar");
            Preferences.Remove("Logueado");
            Application.Current.MainPage = new LoginPage();
            return;
        }
        else if (rpta == -1)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Clave Actual no es Correcta", "Cancelar");
            return;
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un Error", "Cancelar");
            return;
        }

    }

    private void btnregresar_Clicked(object sender, EventArgs e)
    {
        App.Navigate.PopAsync();
    }
}