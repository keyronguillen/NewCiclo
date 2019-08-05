using Ciclo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ciclo.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Carrito : ContentPage
	{
        List<Producto> ListaProductos = new List<Producto>();
		public Carrito (string usuario)
		{
			InitializeComponent ();
            lbl_user.Text = "User: " + usuario;
            Cargar(usuario);
        }

        public async void Cargar(string usuario)
        {
            ListaProductos = await App.ProductoPrincipal.MostrarCarrito(usuario);
            if (ListaProductos.Count <= 0)
            {
                lbl_total.Text = "No hay productos en el carrito!";
                btn_pagar.IsVisible = false;
            }
            else
            {
                CarritoLleno.ItemsSource = ListaProductos;
                double total = (await App.ProductoPrincipal.Suma(usuario)).Sum(x => x.PrecioProducto);
                lbl_total.Text = "Total a pagar: " + total.ToString();
            } 
        }

        private async void Btn_pagar_Clicked(object sender, EventArgs e)
        {
            foreach (Producto producto in ListaProductos)
            {
                int codigo = producto.CodigoProducto;
                await App.ProductoPrincipal.Pagar(codigo);
            }
        }
    }
}