using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace pryReservas.Views;

public partial class vPago : ContentPage
{
    int idUser;
    private const string BaseUrl = "http://192.168.100.84/pryReservas/wspagos.php";
    private int idReserva;
    private decimal precioCancha;

    public vPago(int idUsuario, int intId, decimal dPrecio)
	{
		InitializeComponent();
        idReserva = intId;
        precioCancha = dPrecio;
        txtMontoTotal.Text = "$ " + dPrecio.ToString();
        idUser = idUsuario;
    }

    private void txtMontoCancelado_TextChanged(object sender, TextChangedEventArgs e)
    {
        decimal dMontoRestante;
        if (int.TryParse(e.NewTextValue, out int number))
        {
            if (number > precioCancha)
            {
                ((Entry)sender).Text = precioCancha.ToString();
            }
        }

        if (!Regex.IsMatch(e.NewTextValue, @"^[0-9]*$"))
        {
            ((Entry)sender).Text = "0";
        }

        string strMontoCancelado = txtMontoCancelado.Text;

        if (decimal.TryParse(strMontoCancelado, out decimal dMontoCancelado))
        {
            dMontoRestante = precioCancha - dMontoCancelado;
            txtMontoRestante.Text = dMontoRestante.ToString();
        }
        else
        {
            txtMontoRestante.Text = "0";
        }
    }

    private async void btnPagar_Clicked(object sender, EventArgs e)
    {
        HttpClient cliente = new HttpClient();

        if(pkMetodoPago.SelectedItem == null)
        {
            await DisplayAlert("Alerta", "Debe escoger un método de pago", "Salir");
            return;
        }
        if(string.IsNullOrEmpty(txtMontoCancelado.Text) || string.IsNullOrEmpty(txtMontoRestante.Text)) 
        {
            txtMontoCancelado.Text = "0";
            txtMontoRestante.Text = precioCancha.ToString();
        }

        Dictionary<string, string> parametros = new Dictionary<string, string>
        {
            { "idreserva", idReserva.ToString() },
            { "montopagado", txtMontoCancelado.Text},
            { "montorestante", txtMontoRestante.Text},
            { "fechapago", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
            {"metodo", pkMetodoPago.SelectedItem.ToString() }

        };

        var contenido = new FormUrlEncodedContent(parametros);

        try
        {
            HttpResponseMessage respuesta = await cliente.PostAsync(BaseUrl, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Atención", "Gracias por su pago", "Salir");
                await Navigation.PushAsync(new vCalificacion(idReserva, idUser));
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