using System;
using System.Collections.Generic;
using System.Text;

namespace Cliente_WPF.Models
{
    /// <summary>
    /// Objeto con la información para regitrar o actualizar un producto en el inventario 
    /// </summary>
    public class ProductoInventarioViewModel
        {
            public int Id { get; set; }
            public int IdProducto { get; set; }
            public int IdSucursal { get; set; }
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }
            public ProductoInventarioViewModel()
            {
                IdProducto = 0;
                IdSucursal = 0;
                Precio = 0;
                Cantidad = 0;
            }
        }
    }

