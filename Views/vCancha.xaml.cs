using Newtonsoft.Json;
using pryReservas.Models;
using System.Collections.ObjectModel;

namespace pryReservas.Views;

public partial class vCancha : ContentPage
{
    private const string url = "http://192.168.100.84/pryReservas/wscancha.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Canchas> cancha;
    List<Canchas> canchas;
    int IdUsuario;
    public vCancha(int IdUser)
    {
        InitializeComponent();
        ObtenerDatos();
        IdUsuario = IdUser;

    }

    public async void ObtenerDatos()
    {
        var content = await cliente.GetStringAsync(url);
        canchas = JsonConvert.DeserializeObject<List<Canchas>>(content);

        var NCancha = canchas.Select(p => p.IdCancha).ToList();

        // Agregar los nombres al Picker
        foreach (var numero in NCancha)
        {
            pckCancha.Items.Add(numero.ToString());
        }
        pckCancha.SelectedIndex = 0;
        pckCancha.SelectedIndexChanged += pckCancha_SelectedIndexChanged;
    }

    private void btnSeleccionar_Clicked(object sender, EventArgs e)
    {
        int IdCancha = 0;
        IdCancha = Convert.ToInt32(pckCancha.SelectedItem);
        decimal Precio = 0;
        Precio = Convert.ToDecimal(lblPrecio.Text);

        Navigation.PushAsync(new vReservas(IdUsuario, IdCancha, Precio));
    }

    private void pckCancha_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedCancha = Convert.ToInt32(pckCancha.SelectedItem);
        var fila= canchas.FirstOrDefault(c => c.IdCancha == selectedCancha);
        if (fila != null)
        {
            lblTipoCesped.Text = fila.TipoCesped;
            lblCapacidad.Text = fila.Capacidad.ToString();
            lblPrecio.Text = fila.Tarifa.ToString();

            switch (fila.IdCancha)
            {
                case 1:
                    imgCancha.Source = "cancha1.jpg";
                    break;
                case 2:
                    imgCancha.Source = "cancha2.jpg";
                    break;
                case 3:
                    imgCancha.Source = "cancha3.jpg";
                    break;
                case 4:
                    imgCancha.Source = "cancha4.jpg";
                    break;
                case 5:
                    imgCancha.Source = "cancha5.jpg";
                    break;
                default:
                    imgCancha.Source = "cancha1.jpg";
                    break;
            }
        }
    }

}