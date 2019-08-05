using Ciclo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ciclo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
	{

		public Login ()
		{
			InitializeComponent ();
            btn_ingresar.Clicked += Btn_ingresar_Clicked;
		}

        private async void Btn_ingresar_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_pass.Text))
            {
                lbl_aviso.Text = "Todos los campos son obligatorios!";
            }
            else
            {
                List<Persona> persona = await App.PersonaLogin.ComprobarUsuario(txt_user.Text, txt_pass.Text);
                if (persona.Count() <= 0)
                {
                    lbl_aviso.Text = "Inicio de sesión no permitida, debe registrarse primero!";
                    txt_pass.Text = "";
                    txt_user.Text = "";
                }
                else
                {
                    string usuario = txt_user.Text;
                    txt_pass.Text = "";
                    txt_user.Text = "";
                    await Navigation.PushAsync(new MainPage(usuario));
                }
                
            }
        }

        private async void Btn_registro_Clicked(object sender, EventArgs e)
        {
            txt_pass.Text = "";
            txt_user.Text = "";
            lbl_aviso.Text = "";
            await Navigation.PushAsync(new Registro());
        }

        private async void Btn_recuperar_Clicked(object sender, EventArgs e)
        {
            await App.PersonaLogin.Drop();
        }
    }
}