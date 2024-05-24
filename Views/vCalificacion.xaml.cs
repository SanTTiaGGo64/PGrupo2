using Microsoft.Extensions.Configuration;

namespace pryReservas.Views;

public partial class vCalificacion : ContentPage
{
    int reservaid, idUser;
    private int selectedValue = -1;
    private const string BaseUrl = "http://192.168.100.84/pryReservas/wscalificacion.php";

    public vCalificacion(int idReserva, int idUsuario)
    {
        InitializeComponent();
        reservaid = idReserva;
        idUser = idUsuario;
        radioButton1.CheckedChanged += RadioButton_CheckedChanged;
        radioButton2.CheckedChanged += RadioButton_CheckedChanged;
        radioButton3.CheckedChanged += RadioButton_CheckedChanged;
        radioButton4.CheckedChanged += RadioButton_CheckedChanged;
        radioButton5.CheckedChanged += RadioButton_CheckedChanged;
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = (RadioButton)sender;
        if (radioButton.IsChecked)
        {
            // Obtener el valor del radio button seleccionado
            int value = int.Parse(radioButton.Content.ToString());
            selectedValue = value;
        }
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        HttpClient cliente = new HttpClient();

        Dictionary<string, string> parametros = new Dictionary<string, string>
            {
                { "idreserva", reservaid.ToString() },
                { "puntaje", selectedValue.ToString()},
                { "comentario", txtComentario.Text },
                { "fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
            };

        var contenido = new FormUrlEncodedContent(parametros);

        try
        {
            HttpResponseMessage respuesta = await cliente.PostAsync(BaseUrl, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Atención", "Muchas gracias por su valoración", "Salir");
                await Navigation.PushAsync(new vPerfil(idUser));
            }
            else
            {
                Console.WriteLine("Error al enviar la solicitud: " + respuesta.StatusCode);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }
}