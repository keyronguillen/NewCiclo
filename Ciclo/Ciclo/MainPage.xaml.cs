using Ciclo.Modelos;
using Ciclo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ciclo
{
    public partial class MainPage : ContentPage
    {
        public string User { get; set; }
        public MainPage(string usuario)
        {
            InitializeComponent();
            User = usuario;
            lbl_user.Text = "User: " + usuario;
        }

        private async void Btn_bicis_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VerProducto(User, 0));
        }

        private async void Btn_partes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VerProducto(User, 1));
        }

        private async void Btn_carro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Carrito(User));
        }

        private async void Btn_agregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarProducto(User));
        }
    }
}
