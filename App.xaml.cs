using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;

namespace pryReservas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.login());
        }
    }
}
