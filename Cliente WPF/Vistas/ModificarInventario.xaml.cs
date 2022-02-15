using Cliente_WPF.Logic;
using Cliente_WPF.Models;
using Cliente_WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Lógica de interacción para ModificarInventario.xaml
    /// </summary>
    public partial class ModificarInventario : Window
    {
        public ModificarInventario()
        {
            InitializeComponent();
            ObtenerSucursales();
        }

        /// <summary>
        /// Obtener el listado de sucursales
        /// </summary>
        private async void ObtenerSucursales() 
        {
            var respuesta = await InventarioLogic.ObtenerSucursales();
            this.ComboSucursales.ItemsSource = respuesta;
            ObtenerProductos();
        }

        /// <summary>
        /// Obtener el listado de productos
        /// </summary>
        private async void ObtenerProductos()
        {
            var productos = await ProductosLogic.ObtenerProductos();
            var respuesta = await InventarioLogic.VoidGuaradIdProductos(productos);
            this.ComboProductos.ItemsSource = respuesta;
        }

        /// <summary>
        /// Buscar la existencia de productos en una sucursal
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        private async void BuscarInventario(object e, RoutedEventArgs args)
        {
            ObservableCollection<InventarioViewModel> response = new ObservableCollection<InventarioViewModel>();
            string Sucursal = this.ComboSucursales.SelectedItem.ToString();
            string Producto = this.ComboProductos.SelectedItem.ToString();
            if (string.IsNullOrEmpty(Producto))
            {
                response = await InventarioLogic.ObtenerInventarioPorSucursal(Sucursal);
            }
            else {
                response = await InventarioLogic.ObtenerProductosPorSucursal(Sucursal, Producto);
            }

            this.dataGridProductos.ItemsSource = response;
        }

        /// <summary>
        /// Buscar la existencia de productos en una sucursal
        /// </summary>
        private async void BuscarInventario()
        {
            ObservableCollection<InventarioViewModel> response = new ObservableCollection<InventarioViewModel>();
            string Sucursal = this.ComboSucursales.SelectedItem.ToString();
            string Producto = this.ComboProductos.SelectedItem.ToString();
            if (string.IsNullOrEmpty(Producto))
            {
                response = await InventarioLogic.ObtenerInventarioPorSucursal(Sucursal);
            }
            else
            {
                response = await InventarioLogic.ObtenerProductosPorSucursal(Sucursal, Producto);
            }

            this.dataGridProductos.ItemsSource = response;
        }

        /// <summary>
        /// Regresar a la pantalla anterior
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        private void Regresar(object e, RoutedEventArgs args)
        {
            GestionInventario inventario = new GestionInventario();
            inventario.Show();
            this.Close();
        }

        /// <summary>
        /// Cargar la información para crear una nueva sucursal
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        private void CargarNuevaSucursal(object e, RoutedEventArgs args)
        {
            AgregarSucursal sucursal = new AgregarSucursal();
            sucursal.Show();
            this.Close();
        }

        /// <summary>
        /// Agregar la información de un producto a alguna sucursal
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        private async void AgregarProductoAInventario(object e, RoutedEventArgs args)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            var nuevo = (InventarioViewModel)this.dataGridProductos.CurrentItem;
            string sucursal = this.ComboSucursales.SelectedItem.ToString();
            string producto = this.ComboProductos.SelectedItem.ToString();
            if (!(nuevo == null) && (nuevo.Precio > 0) && (nuevo.Cantidad > 0))
            {
                if (InventarioLogic.ValidarNuevoProductoEnInventario(nuevo.Id) == true)
                {
                    respuesta = await InventarioLogic.ActualizarProductoEnInventario(nuevo, sucursal, producto);
                }
                else
                {
                    respuesta = await InventarioLogic.AgregarProductoAInventario(nuevo, sucursal, producto);
                }
                MessageBox.Show(respuesta.Message);
                BuscarInventario();
            }
            else {
                MessageBox.Show("Favor de guardar los datos antes de solicitar la modificación");
            }
        }

        /// <summary>
        /// Eliminar un producto del inventario
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        private async void EliminarProductoDeInventario(object e, RoutedEventArgs args)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            var producto = (InventarioViewModel)this.dataGridProductos.CurrentItem;

            if (producto != null)
            {
                int idProducto = producto.Id;
                respuesta = await InventarioLogic.EliminarProducto(idProducto);
                MessageBox.Show(respuesta.Header);
            }
            else {
                MessageBox.Show("Favor de seleccionar un elemento existente del inventario.");
            }
            
        }
    }
}
