using System;
using System.Collections.Generic;
using System.Text;

namespace Cliente_WPF.Models
{
    /// <summary>
    /// Objeto con la información de los producto
    /// </summary>
    public class ProductosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarras { get; set; }
        public ProductosViewModel() {
            Id = 0;
            Nombre = string.Empty;
            CodigoBarras = string.Empty;
        }
    }
}
