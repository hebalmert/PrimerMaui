using AppMaui.Modelos;
using AppMaui.Models;

namespace AppMaui.Pages;

public partial class Menu : ContentPage
{
	public MenuModel oMenuModel { get; set; }
	public Menu()
	{
		InitializeComponent();
		oMenuModel = new MenuModel();
		oMenuModel.ListaMenu = new List<MenuCLS>();
		//Creamos el Menu
		oMenuModel.ListaMenu.Add(new MenuCLS { nombreIcono = "ic_user", nombreItem = "Usuario"});
        oMenuModel.ListaMenu.Add(new MenuCLS { nombreIcono = "ic_contra", nombreItem = "Cambiar Clave" });
        oMenuModel.ListaMenu.Add(new MenuCLS { nombreIcono = "ic_perfil", nombreItem = "Modificar Perfil" });
        oMenuModel.ListaMenu.Add(new MenuCLS { nombreIcono = "ic_mascota", nombreItem = "Mis Mascotas" });
        oMenuModel.ListaMenu.Add(new MenuCLS { nombreIcono = "citasimagen", nombreItem = "Mis Citas" });
        oMenuModel.ListaMenu.Add(new MenuCLS { nombreIcono = "ic_salir", nombreItem = "Salir" });
        BindingContext = this;
	}

    private async void lstMenu_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		MenuCLS obj = (MenuCLS)e.Item;
        //await Application.Current.MainPage.DisplayAlert("Item Seleccion", obj.nombreItem, "Cancelar");
		switch (obj.nombreItem)
		{
			case "Salir":
				Preferences.Remove("Logueado");
				Application.Current.MainPage = new LoginPage();
				break;

            case "Mis Mascotas":
                await App.Navigate.PushAsync(new MisMascotasPage());
                break;

            case "Cambiar Clave":
                await App.Navigate.PushAsync(new CambiarClavePage());
                break;

            case "Modificar Perfil":
                await App.Navigate.PushAsync(new ModificarPerfilPage());
                break;

            case "Mis Citas":
                await App.Navigate.PushAsync(new MisCitasPage());
                break;

            case "Usuario":
				await App.Navigate.PushAsync(new UsuarioPage()); 
				break;
        }
		App.MenuApp.IsPresented = false;  //De esta Manera ocultamos el menu
    }
}