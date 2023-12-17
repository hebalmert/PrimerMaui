using AppMaui.Modelos;
using AppMaui.Models;
using MyWeb.Clases;
using System.Runtime.InteropServices;

namespace AppMaui.Pages;

public partial class FormMascotaPage : ContentPage
{
	public MascotaModel oMascotaModel { get; set; }

	public string tituloForm { get; set; }

    private string urlBase;
    private string urlMascota;
    private string urlTipoMascota;
    private string token;

    public FormMascotaPage(string titulo, int id)
	{
		InitializeComponent();
        urlBase = App.Current.Resources["urlBase"].ToString();
        urlMascota = App.Current.Resources["urlMascota"].ToString();
        urlTipoMascota = App.Current.Resources["urlTipoMascota"].ToString();
        token = App.Current.Resources["Token"].ToString();
        oMascotaModel = new MascotaModel();
		oMascotaModel.oMascotaCLS = new MascotaCLS();
        oMascotaModel.listatipomascota = new List<TipoMascotaCLS>();
        oMascotaModel.itemTipoMascota = new TipoMascotaCLS();
        tituloForm = titulo;
		BindingContext = this;
        recuperarDatos(id);
	}

    private async void recuperarDatos(int id)
    {
         List<TipoMascotaCLS> listaTipoMascota = await  ClienteHttp.GetAll<TipoMascotaCLS>(urlBase, urlTipoMascota, token);
        listaTipoMascota.Insert(0, new TipoMascotaCLS { iidtipomascota = 0, nombretipomascota = "--Seleccione Tipo--" });
        oMascotaModel.listatipomascota = listaTipoMascota;
        oMascotaModel.itemTipoMascota = oMascotaModel.listatipomascota[0];
        oMascotaModel.itemSexo = "--Seleccione--";
        if (id != 0)
        {
            MascotaCLS obj = await ClienteHttp.Get<MascotaCLS>(urlBase, urlMascota + "recuperarMascota/" + id, token);
            oMascotaModel.oMascotaCLS = obj;
            oMascotaModel.foto = oMascotaModel.oMascotaCLS.foto;
            oMascotaModel.itemTipoMascota = oMascotaModel.listatipomascota.Where(p => p.iidtipomascota == obj.iidtipomascota).First();
            oMascotaModel.itemSexo = obj.iidsexo == 1 ? "Macho" : "Hembra";

        }
    }

    private async void btnAceptarMascota_Clicked(object sender, EventArgs e)
    {
        int idusuario = Preferences.Get("iidusuario", 0);
        oMascotaModel.oMascotaCLS.iidusuariopropietario = idusuario;
        oMascotaModel.oMascotaCLS.iidtipomascota = oMascotaModel.itemTipoMascota.iidtipomascota;
        oMascotaModel.oMascotaCLS.iidsexo = oMascotaModel.itemSexo == "Macho" ? 1 : 2;
        int rpta = await ClienteHttp.Post<MascotaCLS>(urlBase, urlMascota, oMascotaModel.oMascotaCLS, token);
        if (rpta == 1)
        {
            await Application.Current.MainPage.DisplayAlert("Exito", "Se Modifico la Mascota", "Aceptar");
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

    private void tabImageClic_Tapped(object sender, TappedEventArgs e)
    {
        SeleccionarFotoGaleria();
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
                oMascotaModel.foto = buffer;
                oMascotaModel.oMascotaCLS.foto = buffer;
                oMascotaModel.oMascotaCLS.nombrefoto = foto.FileName;
            }
        }
    }
}