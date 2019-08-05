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
    public partial class VerProducto : ContentPage
    {
        public int codigo;
        public string user;
		public VerProducto (string usuario, int tipo_producto)
		{
			InitializeComponent ();
            lbl_user.Text = "User: " + usuario;
            user = usuario;
            if (tipo_producto == 0)
            {
                lbl_title.Text = "Bikes";
            }
            else
            {
                lbl_title.Text = "Parts";
            }
            Cargar(tipo_producto);
        }

        public async void Cargar(int Tipo_Producto)
        {
            List<Producto> ListaProductos = await App.ProductoPrincipal.ObtenerProductos(Tipo_Producto);
            Productos.ItemsSource = ListaProductos;
        }

        private async void Btn_Agregar_Clicked(object sender, EventArgs e)
        {
            if (codigo != 0)
            {
                await App.ProductoPrincipal.AgregarCarrito(codigo, 1, user);
                codigo = 0;
                statusMessage.Text = "";
            }
            else
            {
                statusMessage.Text = "Debe seleccionar un producto";
            }
        }

        private void Productos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Producto elem = e.SelectedItem as Producto;
            statusMessage.Text = "Producto seleccionado: " + elem.CodigoProducto.ToString();
            codigo = elem.CodigoProducto;
        }
    }
}