using Cliente_WPF.Models;
using Cliente_WPF.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cliente_WPF.Services;
using Cliente_WPF.Vistas;

namespace Cliente_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Redireccionar a la ventana para gestionar el inventario
        /// </summary>
        /// <param name="sender">Componente de la vista</param>
        /// <param name="e"></param>
        private void irInventario(object sender, RoutedEventArgs e)
        {
            GestionInventario inventario = new GestionInventario();
            inventario.Show();

            this.Close();
        }
    }
}
