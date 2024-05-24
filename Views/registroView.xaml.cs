using System.Net;

namespace pryReservas.Views;

public partial class Registro : ContentPage
{
    public Registro()
    {
        InitializeComponent();
    }
    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmacion = await DisplayAlert("Confirmar", "¿Está seguro de guardar?", "Sí", "No");

            if (confirmacion)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    var datosUsuario = new Dictionary<string, string>
                {
                    { "nombre", txtNombre.Text },
                    { "apellido", txtApellido.Text },
                    { "correo", txtCorreo.Text },
                    { "contrasena", txtPassword.Text },
                    { "telefono", txtCelular.Text }
                };

                    var contenido = new FormUrlEncodedContent(datosUsuario);

                    HttpResponseMessage respuesta = await cliente.PostAsync("http://192.168.100.84/pryReservas/wsusuario.php", contenido);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Ocurrió un error al guardar los datos", "Cerrar");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
        await Navigation.PushAsync(new login());
    }
}