using AppMaui.Modelos;
using AppMaui.Models;

namespace AppMaui.Pages;

public partial class Details : ContentPage
{
    public OpcionesPrinciplaModel oOpcionesPrinciplaModel { get; set; }
    public Details()
	{
		InitializeComponent();
		oOpcionesPrinciplaModel = new OpcionesPrinciplaModel();
        oOpcionesPrinciplaModel.listopcionesprincipal = new List<OpcionesPrincipalCLS>();
        oOpcionesPrinciplaModel.listopcionesprincipal.Add(new OpcionesPrincipalCLS 
        { 
            nombreimagen = "mascotaimagen", 
            titulo = "Mis Mascotas"
        });
        oOpcionesPrinciplaModel.listopcionesprincipal.Add(new OpcionesPrincipalCLS
        {
            nombreimagen = "citasimagen",
            titulo = "Mis Cita"
        });
        oOpcionesPrinciplaModel.listopcionesprincipal.Add(new OpcionesPrincipalCLS
        {
            nombreimagen = "historialimagen",
            titulo = "Historia Cita"
        });
        oOpcionesPrinciplaModel.listopcionesprincipal.Add(new OpcionesPrincipalCLS
        {
            nombreimagen = "perfilimagen",
            titulo = "Mi Perfil"
        });
        BindingContext = this;
	}

    private void tabOpcionPrincipal_Tapped(object sender, TappedEventArgs e)
    {
        string nombre = ((TappedEventArgs)e).Parameter.ToString();
        switch (nombre) 
        {
            case "mascotaimagen":
                App.Navigate.PushAsync(new MisMascotasPage());
                break;

            case "perfilimagen":
                App.Navigate.PushAsync(new UsuarioPage());
                break;
            case "citasimagen":
                App.Navigate.PushAsync(new MisCitasPage());
                break;
        }
    }
}