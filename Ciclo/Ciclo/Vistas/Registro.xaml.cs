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
    public partial class Registro : ContentPage
    {
        public Boolean ContraTrue { get; set; }
        public Boolean UserTrue { get; set; }
        public string StatusMessage { get; set; }
        public Registro()
        {
            InitializeComponent();
        }

        private async void Btn_ingresar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_pass.Text) || string.IsNullOrEmpty(txt_pass2.Text) || string.IsNullOrEmpty(txt_email.Text))
            {
                statusMessage.Text = "Todos los campos son obligatorios!";
            }
            else
            {
                if (ContraTrue == false)
                {
                    statusMessage.Text = "Las contraseñas deben ser iguales!";
                }
                else if(UserTrue == false){
                    statusMessage.Text = "Usuario no disponible!";
                }
                else
                {
                    statusMessage.Text = "";
                    await App.PersonaLogin.AgregarNuevoUsuario(txt_user.Text, txt_pass.Text, txt_email.Text);
                    statusMessage.Text = App.PersonaLogin.StatusMessage;
                    await Navigation.PopAsync();
                }
            }
        }

        private void Txt_pass2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_pass.Text == txt_pass2.Text)
            {
                ContraTrue = true;
                statusMessage.Text = "";
            }
            else
            {
                ContraTrue = false;
                statusMessage.Text = "Las contraseñas deben ser iguales!";
            }
        }
        private async void Txt_user_Unfocused(object sender, FocusEventArgs e)
        {
            List<Persona> persona = await App.PersonaLogin.Usuario(txt_user.Text);
            if (persona.Count > 0)
            {
                UserTrue = false;
                statusMessage.Text = "Usuario no disponible!";
            }
            else
            {
                UserTrue = true;
                statusMessage.Text = "";
            }
        }
    }
}