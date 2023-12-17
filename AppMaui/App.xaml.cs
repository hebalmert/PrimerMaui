using AppMaui.Pages;

namespace AppMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Preferences.Get("Logueado", false) == true)
            {
                MainPage = new PrincipalPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        //Esta propiedad se cree desde PrincipalPage con App.MenuApp y dando Click en el Foco y Crear Propiedad
        public static PrincipalPage MenuApp { get; internal set; }
        public static NavigationPage Navigate { get; internal set; }
    }
}
