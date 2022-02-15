using Cliente_WPF.Logic;
using Cliente_WPF.Models;
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
    /// Lógica de interacción para AgregarSucursal.xaml
    /// </summary>
    public partial class AgregarSucursal : Window
    {
        public AgregarSucursal()
        {
            InitializeComponent();
        }

        public void Regresar(object sender, RoutedEventArgs args)
        {
            ModificarInventario inventario = new ModificarInventario();
            inventario.Show();
            this.Close();
        }

        public async void CrearSucursal(object sender, RoutedEventArgs args)
        {
            var sucursal = this.TextSucursal.Text.Trim().ToString();
            ServerResponseViewModel response = await InventarioLogic.AgregarSucursal(sucursal);
            MessageBox.Show(response.Message);
        }
    }
}
