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
	public partial class AgregarProducto : ContentPage
	{
        public Boolean ProductoExiste { get; set; }
        public AgregarProducto (string usuario)
		{
			InitializeComponent ();
            lbl_user.Text = "User: " + usuario;
        }

        private async void Btn_guardar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text) || string.IsNullOrEmpty(txt_descr.Text) || string.IsNullOrEmpty(txt_stock.Text) || string.IsNullOrEmpty(txt_price.Text) || pik_kind.SelectedIndex < 0)
            {
                statusMessage.Text = "Todos los campos son obligatorios!";
            }
            else
            {
                if (ProductoExiste == false)
                {
                    statusMessage.Text = "Este producto ya está registrado, puede insertar uno nuevo con otro nombre o actualizar el existente!";
                }
                else
                {
                    statusMessage.Text = "";
                    await App.ProductoPrincipal.AgregarPruducto(txt_name.Text, txt_descr.Text, Convert.ToInt32(txt_stock.Text), Convert.ToDouble(txt_price.Text), pik_kind.SelectedIndex);
                    statusMessage.Text = App.PersonaLogin.StatusMessage;
                    txt_name.Text = "";
                    txt_descr.Text = "";
                    txt_stock.Text = "";
                    txt_price.Text = "";
                    pik_kind.SelectedIndex = -1;
                }
            }
        }

        private async void Txt_name_Unfocused(object sender, FocusEventArgs e)
        {
            List<Producto> persona = await App.ProductoPrincipal.Producto(txt_name.Text);
            if (persona.Count > 0)
            {
                ProductoExiste = false;
                statusMessage.Text = "Este producto ya está registrado, puede insertar uno nuevo con otro nombre o actualizar el existente!";
            }
            else
            {
                ProductoExiste = true;
                statusMessage.Text = "";
            }
        }
    }
}