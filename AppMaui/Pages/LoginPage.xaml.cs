using AppMaui.Models;
using MyWeb.Clases;

namespace AppMaui.Pages;

public partial class LoginPage : ContentPage
{
    public LoginModel LoginModel { get; set; }

    private string urlBase; 
    private string urlLogin;


    public LoginPage()
    {
        InitializeComponent();
        LoginModel = new LoginModel();
        BindingContext = this;
    }

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        LoginModel.IsCargando = true;

        urlBase = App.Current.Resources["urlBase"].ToString();
        urlLogin = App.Current.Resources["urlLogin"].ToString();
        int iidusuario = await ClienteHttp.GetInt(urlBase, urlLogin + LoginModel.NombreUsuario +"/" + LoginModel.Contra);

        if (iidusuario == 0)
        {
            await Application.Current.MainPage.DisplayAlert("Usuario", "Usuario o Contrasena Incorrecto", "Cancelar");
            LoginModel.IsCargando = false;
            Preferences.Default.Set("Logueado", false);
        }
        else
        {
            Preferences.Default.Set("Logueado", true);
            Preferences.Default.Set("iidusuario", iidusuario);
            Preferences.Default.Set("nombreusuario", LoginModel.NombreUsuario);
            LoginModel.IsCargando = false;
            Application.Current.MainPage = new PrincipalPage();
        }
    }
}