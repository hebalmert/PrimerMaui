namespace AppMaui.Pages;

public partial class PrincipalPage : FlyoutPage
{
	public PrincipalPage()
	{
		InitializeComponent();
		int idusuario= Preferences.Get("iidusuario", 0);
		string nombreusuario = Preferences.Get("nombreusuario", "");
		//lblUsuario.Text = idusuario + " - " + nombreusuario;
		App.MenuApp = this;  //Le damos Click en el Foco, y de decimos Crear Propiedad para poderla acceder desde cualquier sitio
		App.Navigate = Navigate;  // en el Xaml en la propiedad NvigatePage tenemos en x:Name="Navigate", entonces creamos la propiedad y la 
		                          //igualamos a una nueva App.Propiedad.  Siempre revisamos en App.Xaml  para ver si se crearon

    }

    private void btnCerrarSesion_Clicked(object sender, EventArgs e)
    {
		Preferences.Remove("Logueado");
        Application.Current.MainPage = new LoginPage();
    }
}