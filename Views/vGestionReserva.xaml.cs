
using Newtonsoft.Json;
using pryReservas.Models;
using System.Collections.ObjectModel;
using System.Net;

namespace pryReservas.Views;

public partial class vGestionReserva : ContentPage
{
    int IdUsuario;
    private const string url = "http://192.168.100.84/pryReservas/wsreservas.php";
    private const string urlpago = "http://192.168.100.84/pryReservas/wspagos.php";
    private const string urlcalif = "http://192.168.100.84/pryReservas/wscalificacion.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Reservas> reserva;
    List<Reservas> listReservas;
    public vGestionReserva(int IdUser)
    {
        InitializeComponent();
        ObtenerReservaUsuario();
        IdUsuario = IdUser;
    }

    private void pckReservas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedReserva = Convert.ToInt32(pckReservas.SelectedItem);
        var fila = listReservas.FirstOrDefault(c => c.IdReserva == selectedReserva);
        if (fila != null)
        {
            dtFecha.Date = fila.FechaReserva;
            txtHoraEntrada.Text = fila.HoraEntrada.ToString();
            txtHoraSalida.Text = fila.HoraSalida.ToString();

        }
    }
    public async void ObtenerReservaUsuario()
    {
        try
        {
            var content = await cliente.GetStringAsync(url);
            listReservas = JsonConvert.DeserializeObject<List<Reservas>>(content);

            var NCancha = listReservas.Where(p => p.IdUsuario == IdUsuario).Select(p => p.IdReserva).ToList();

            foreach (var numero in NCancha)
            {
                pckReservas.Items.Add(numero.ToString());
            }
            pckReservas.SelectedIndex = 0;
        }
        catch(Exception ex)
        {
            await DisplayAlert("Alerta", "No tiene reservas", "Salir");
            await Navigation.PushAsync(new vPerfil(IdUsuario));
        }
       
    }
    private async void btnEliminarReserva_Clicked(object sender, EventArgs e)
    {
        try
        {
            string strCodigo = pckReservas.SelectedItem.ToString();
            string urlDeleteReserva = url + "?IdReserva=" + strCodigo;
            string urlDeletePagos = urlpago + "?IdPago=" + strCodigo;
            string urlDeleteCalificacion = urlcalif + "?IdCalificacion=" + strCodigo;

            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();

            cliente.UploadValues(urlDeletePagos, "DELETE", parametros);
            cliente.UploadValues(urlDeleteCalificacion, "DELETE", parametros);
            cliente.UploadValues(urlDeleteReserva, "DELETE", parametros);

            await DisplayAlert("Alerta", "Registro eliminado", "Cerrar");
            Navigation.PushAsync(new vPerfil(IdUsuario));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo eliminar la reserva: " + ex.Message, "Cerrar");
        }

    }
    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
        string strCodigo = pckReservas.SelectedItem.ToString();
        string dtfecha = dtFecha.Date.ToString("yyyy-MM-dd");
        int hEntrada = Convert.ToInt32(txtHoraEntrada.Text);
        int hSalida = Convert.ToInt32(txtHoraSalida.Text);
        string urlUpdate = url + "?IdReserva=" + strCodigo + "&FechaReserva=" + dtfecha + "&HoraEntrada=" + hEntrada + "&HoraSalida=" + hSalida;

        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();

            cliente.UploadValues(urlUpdate, "PUT", parametros);
            DisplayAlert("Alerta", "Reserva actualizada", "Cerrar");
            Navigation.PushAsync(new vPerfil(IdUsuario));
        }
        catch (Exception ex)
        {
            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }

}