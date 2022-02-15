using System;
using System.Collections.Generic;
using System.Text;

namespace Cliente_WPF.Models
{
    /// <summary>
    /// Objeto con la información de las sucursales 
    /// </summary>
    public class SucursalViewModel
    {
        public int Id {get; set;}
        public string Sucursal { get; set; }
        public SucursalViewModel() 
        {
            Id = 0;
            Sucursal = string.Empty;
        }
    }
}
