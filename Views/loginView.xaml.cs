using Newtonsoft.Json;
using pryReservas.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace pryReservas.Views;

public partial class login : ContentPage
{
    private const string url = "http://192.168.100.84/pryReservas/wsusuario.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<usuarioModel> usuarios;
    List<usuarioModel> users;
    public login()
	{
		InitializeComponent();
        ObtenerDatos();

    }
    private async void CreateAccount_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new Registro());
    }
    private async void Login_Clicked(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        int posicion = -1;

        var result = users.FirstOrDefault(u => u.correo.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (password == result.contraseña)
        {
            await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
            await Navigation.PushAsync(new vPerfil(result.IdUsuario));
        }
        else
        {
            await DisplayAlert("Error", "Inicio de sesión fallido", "OK");
        }
    }
    public async void ObtenerDatos()
    {
        var content = await cliente.GetStringAsync(url);
        users = JsonConvert.DeserializeObject<List<usuarioModel>>(content);

        //var NCancha = users.Select(p => p.IdCancha).ToList();
    }
}