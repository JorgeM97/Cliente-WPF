using Cliente_WPF.Models;
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
    /// Lógica de interacción para GestionProductos.xaml
    /// </summary>
    public partial class GestionProductos : Window
    {
        public GestionProductos()
        {
            InitializeComponent();
            Consultar_Informacion();
        }
        /// <summary>
        /// Consultar la información de los productos
        /// </summary>
        private async void Consultar_Informacion()
        {
            var respuesta = await ProductosLogic.ObtenerProductos();
            this.dataGridProductos.ItemsSource = respuesta;
        }

        /// <summary>
        /// Consultar la información de los productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Consultar_Informacion(object sender, RoutedEventArgs e)
        {
            var respuesta = await ProductosLogic.ObtenerProductos();
            this.dataGridProductos.ItemsSource = respuesta;
        }

        /// <summary>
        /// Registrar la infoamción de un nuevo producto o actualziar la de uno existente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RegistrarDatos(object sender, RoutedEventArgs e)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            var nuevo = (ProductosViewModel)this.dataGridProductos.CurrentItem;
            if (!string.IsNullOrEmpty(nuevo.Nombre) || !string.IsNullOrEmpty(nuevo.CodigoBarras))
            {
                if (ProductosLogic.ValidarNuevoProducto(nuevo.Id) == true)
                    respuesta = await ProductosLogic.ActualizarProducto(nuevo);
                else
                    respuesta = await ProductosLogic.AgregarProducto(nuevo);
                MessageBox.Show(respuesta.Header);
                Consultar_Informacion();
            }
            else { MessageBox.Show("Favor de guardar los valores antes de solicitar la modificaicón."); }            
        }

        /// <summary>
        /// regresar al menú de gestión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Regresar(object sender, RoutedEventArgs e)
        {
            GestionInventario gestion = new GestionInventario();
            gestion.Show();
            this.Close();
        }
    }
}
