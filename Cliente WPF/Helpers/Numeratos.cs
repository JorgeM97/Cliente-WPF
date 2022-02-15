using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cliente_WPF.Helpers
{
    public class Numeratos
    {
        public enum Errores
        {
            [Description("Error al registrar la nueva sucursal.")]
            ERROR_CREACION_SUCURSAL = 1001,
            [Description("Error al agregar el producto al inventario.")]
            ERROR_PRODUCTO_INVENTARIO = 1002,
            [Description("Error al actualizar la información del inventario.")]
            ERROR_ACTUALIZACION_INVENTARIO = 1003,
            [Description("Error al eliminar el producto del inventario.")]
            ERROR_ELIMINACION_INVENTARIO = 1004,
            [Description("Error al crear el nuevo producto.")]
            ERROR_CREACION_PRODUCTO = 1005,
            [Description("Error al actualizar la información del producto.")]
            ERROR_ACTUALIZACION_PRODUCTO = 1006
        }
    }
}
