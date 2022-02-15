using Cliente_WPF.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cliente_WPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para GestionInventario.xaml
    /// </summary>
    public partial class GestionInventario : Window
    {
        public GestionInventario()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Regresar a la página de incio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routed"></param>
        private void Regresar(object sender, RoutedEventArgs routed)
        {
            MainWindow atras = new MainWindow();
            atras.Show();
            this.Close();
        }

        /// <summary>
        /// Ir a la gestión de productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routed"></param>
        private void GestionProductos(object sender, RoutedEventArgs routed)
        {
            GestionProductos productos = new GestionProductos();
            productos.Show();
            this.Close();
        }

        /// <summary>
        /// Ir a la gestión de inventarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routed"></param>
        private void ModificarInventario(object sender, RoutedEventArgs routed)
        {
            ModificarInventario inventario = new ModificarInventario();
            inventario.Show();
            this.Close();
        }

        //private void Ventas(object sender, RoutedEventArgs routed)
        //{
        //    Ventas venta = new Ventas();
        //    venta.Show();
        //    this.Close();
        //}
    }
}
