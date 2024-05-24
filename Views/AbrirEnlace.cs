using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace pryReservas.Views
{
    public class AbrirEnlace
    {
        public async void AbrirEnlaceWeb(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                await Launcher.OpenAsync(uri);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }
        }
    }
}