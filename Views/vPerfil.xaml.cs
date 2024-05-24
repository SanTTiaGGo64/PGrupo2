namespace pryReservas.Views;
using Microsoft.Maui.ApplicationModel;
public partial class vPerfil : ContentPage
{
    int IdUsuario;
	public vPerfil(int IdUser)
	{
		InitializeComponent();
        IdUsuario=IdUser;
	}

    private void btnReservar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vReservas(IdUsuario, 0,0));
    }

    private void btnUbicacion_Clicked(object sender, EventArgs e)
    {
        string url = "https://www.google.com/maps/dir//Av.+Mariana+de+Jes%C3%BAs,+Quito+170147/@-0.1814184,-78.590274,12z/data=!4m8!4m7!1m0!1m5!1m1!1s0x91d59af7a52caccd:0xf9f10198972662e2!2m2!1d-78.507872!2d-0.1814186?entry=ttu";
        AbrirEnlace abrir = new AbrirEnlace();
        abrir.AbrirEnlaceWeb(url);
    }
    
    private void btnConsultarReserva_Clicked(object sender, EventArgs e)
    {

        Navigation.PushAsync(new vGestionReserva(IdUsuario));
    }
}