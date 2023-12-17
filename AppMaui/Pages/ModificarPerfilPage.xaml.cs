using AppMaui.Modelos;
using AppMaui.Models;
using MyWeb.Clases;

namespace AppMaui.Pages;

public partial class ModificarPerfilPage : ContentPage
{
    public PersonaModel oPersonaModel { get; set; }

    private string urlBase;
    private string urlUsuario;
    private string urlPersona;
    private string token;

    public ModificarPerfilPage()
    {
        InitializeComponent();
        urlBase = App.Current.Resources["urlBase"].ToString();
        urlUsuario = App.Current.Resources["urlUsuario"].ToString();
        urlPersona = App.Current.Resources["urlPersona"].ToString();
        token = App.Current.Resources["Token"].ToString();
        oPersonaModel = new PersonaModel();
        oPersonaModel.oPersonaCLS = new PersonaCLS();
        BindingContext = this;
        RecuperarUIsuario();
    }

    private async void btnModificarPerfil_Clicked(object sender, EventArgs e)
    {
        int rpta = await ClienteHttp.Post<PersonaCLS>(urlBase, urlPersona, oPersonaModel.oPersonaCLS, token);
        if (rpta == 1)
        {
            await Application.Current.MainPage.DisplayAlert("Exito", "Se Modifico el Perfil", "Aceptar");
            Application.Current.MainPage = new PrincipalPage();

        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un Error", "Aceptar");
        }
    }

    private async void RecuperarUIsuario()
    {
        int idusuario = Preferences.Get("iidusuario", 0);
        PersonaCLS oPersonaCLS = await ClienteHttp.Get<PersonaCLS>(urlBase, urlUsuario + "recuperarPersonaPorIdUsuario/" + idusuario, token);
        oPersonaModel.oPersonaCLS = oPersonaCLS;
        oPersonaModel.imagen = oPersonaModel.oPersonaCLS.archivo;
        //oPersonaModel.imagen = oPersonaCLS.archivo == null ? "noimagen.jpg" : ImageSource.FromStream(() => new MemoryStream(oPersonaCLS.archivo));
    }

    private void tabImageClic_Tapped(object sender, TappedEventArgs e)
    {
        SeleccionarFotoGaleria();
        //Application.Current.MainPage.DisplayAlert("Error", "se dio un click", "Aceptar");
    }

    private async void SeleccionarFotoGaleria()
    {
        //Permiso para leer el Storage del celular
        var status = await Permissions.RequestAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No Esta Habilitado para Leer el Storage", "Aceptar");
            return;
        }
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult foto = await MediaPicker.Default.PickPhotoAsync();
            if (foto != null)
            {
                byte[] buffer = File.ReadAllBytes(foto.FullPath);
                //ImageSource oImageSource = ImageSource.FromStream(() => new MemoryStream(buffer));
                oPersonaModel.imagen = buffer;
                oPersonaModel.oPersonaCLS.archivo = buffer;
                oPersonaModel.oPersonaCLS.nombrearchivo = foto.FileName;
            }
        }
    }

    private void btnregresar_Clicked(object sender, EventArgs e)
    {
        App.Navigate.PopAsync();
    }
}