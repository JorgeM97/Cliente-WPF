using System;
using System.Collections.Generic;
using System.Text;

namespace Cliente_WPF.Helpers
{
    public static class VariablesEstaticas
    {
        //Tipo de respuesta
        public static string TipoRespuestaJson = "json/application";

        //Cadena de cóneción API
        public static string CadenaConexionAPI = "https://localhost:44372/";

        //End Point's Productos
        public static string ObtenerProductos = "/Inventario/ObtenerProductos";
        public static string AgregarProducto = "/Inventario/AgregarNuevoProducto";
        public static string ActualizarProducto = "/Inventario/ActualizarProducto";

        //End Point's Inventario
        public static string ObtenerSucursales = "/Inventario/ObtenerSucursales";
        public static string AgregarSucursal = "/Inventario/AgregarSucursal";
        public static string ObtenerInventarioSucursal = "/Inventario/ObtenerInventarioSucursal";
        public static string ObtenerProductosSucursal = "/Inventario/ObtenerProductisSucursal";
        public static string EliminarInventario = "Inventario/EliminarInventario";
        public static string AgregarProductoInventario = "/Inventario/AgregarProductoAInventario";
        public static string ActualizarProductoInventario = "/Inventario/ActualizarProductoEnInventario";

        //End Point's Ventas
        public static string RegistrarVenta = "/Ventas/RegistrarVenta";
        public static string ObtenerVentasSucursal = "/Ventas/ObtenerVentasSucursal";
        public static string ObtenerVentasProducto = "/Ventas/ObtenerVentasProducto"; 

        //Variables estaticas
        public static string IdSucursal = "idSucursal";
        public static string IdProducto = "idProducto";
    }
}
