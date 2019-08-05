using Ciclo.VistaModelo;
using Ciclo.Vistas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Ciclo
{
    public partial class App : Application
    {
        public static VistaModeloLogin PersonaLogin { get; private set; }
        public static VistaModeloPrincipal ProductoPrincipal { get; private set; }
        public App(string dbPath)
        {
            InitializeComponent();
            PersonaLogin = new VistaModeloLogin(dbPath);
            ProductoPrincipal = new VistaModeloPrincipal(dbPath);
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
