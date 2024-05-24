using Newtonsoft.Json;
using pryReservas.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Net;

namespace pryReservas.Views;

public partial class vReservas : ContentPage
{
    private const string BaseUrl = "http://192.168.100.84/pryReservas/wsreservas.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Reservas> reserva;
    int IdCancha, IdUsuario;
    decimal Precio;
    int idReserva;

    public vReservas(int IdUser, int Cancha, decimal PrecioCancha)
    {
        InitializeComponent();
        ObtenerDatos();
        LlenarPickerHoras();
        IdCancha = Cancha;
        Precio = PrecioCancha;
        lblIdCancha.Text = Cancha.ToString();
        lblPrecio.Text = PrecioCancha.ToString();
        IdUsuario = IdUser;
        
    }
    public async Task ObtenerDatos()
    {
        var content = await cliente.GetStringAsync(BaseUrl);
        List<Reservas> reservas = JsonConvert.DeserializeObject<List<Reservas>>(content);
        reserva = new ObservableCollection<Reservas>(reservas);
        DataTable dataTable = ConvertToDataTable(reservas);
        idReserva = GetLastId(dataTable);
    }

    public static DataTable ConvertToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        // Obtener todas las propiedades de la clase T
        var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        // Crear columnas en el DataTable para cada propiedad
        foreach (var prop in props)
        {
            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        // Añadir filas al DataTable
        foreach (var item in items)
        {
            var values = new object[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

    public static int GetLastId(DataTable dataTable)
    {
        // Asegúrate de que el DataTable tiene filas
        if (dataTable.Rows.Count == 0)
        {
            throw new InvalidOperationException("El DataTable no contiene filas.");
        }

        // Obtener la última fila
        DataRow lastRow = dataTable.Rows[dataTable.Rows.Count - 1];
        int lastId = (int)lastRow["IdReserva"];

        return lastId;
    }

    private void LlenarPickerHoras()
    {
        var HEntrada = new List<string>
            {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"};

        var HSalida = new List<string>
            {"11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22"};

        pckEntrada.ItemsSource = HEntrada;
        pckEntrada.SelectedIndex = 0;

        pckSalida.ItemsSource = HSalida;
        pckSalida.SelectedIndex = 0;
    }

    private async void btnReservar_Clicked(object sender, EventArgs e)
    {
        if(lblIdCancha.Text == "0")
        {
            await DisplayAlert("Alerta", "Debe escoger una cancha", "Salir");
            return;
        }
        int IdCancha = Convert.ToInt32(lblIdCancha.Text);
        int Estado = 1;
        int totalHoras = Convert.ToInt32(pckSalida.SelectedItem) - Convert.ToInt32(pckEntrada.SelectedItem);
        decimal preciototal = (decimal)(Precio * totalHoras);


        HttpClient cliente = new HttpClient();
        Dictionary<string, string> parametros = new Dictionary<string, string>
        {
            { "IdUsuario", IdUsuario.ToString() },
            { "IdCancha", IdCancha.ToString()},
            { "FechaReserva", pckFechaReserva.Date.ToString("yyyy-MM-dd HH:mm:ss") },
            { "HoraEntrada", pckEntrada.SelectedItem.ToString()},
            { "HoraSalida", pckSalida.SelectedItem.ToString() },
            { "Estado",  Estado.ToString()}

        };

        var contenido = new FormUrlEncodedContent(parametros);

        try
        {
            HttpResponseMessage respuesta = await cliente.PostAsync(BaseUrl, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Alerta", "Reserva exitosa!!", "Cerrar");
                await ObtenerDatos();
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
        finally
        {
            await Navigation.PushAsync(new vPago(IdUsuario, idReserva, preciototal));
        }
    }

    private void btnCancha_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vCancha(IdUsuario));
    }
}

