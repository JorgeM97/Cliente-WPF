using Cliente_WPF.Helpers;
using Cliente_WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cliente_WPF.Logic
{
    public class InventarioLogic
    {
        /// <summary>
        /// Objetos estaticos arreglos con la información de las sucursales, productos e inventarios
        /// </summary>
        private static Dictionary<int, string> idsSucursales = new Dictionary<int, string>();
        private static Dictionary<int, string> idsProductos = new Dictionary<int, string>();
        private static List<int> idsInventario = new List<int>();

        /// <summary>
        /// Obtener el listado de sucursales
        /// </summary>
        /// <returns>Listado de sucursales</returns>
        public static async Task<List<string>> ObtenerSucursales()
        {
            List<string> respuesta = new List<string>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble((1000000)));

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await client.GetAsync(VariablesEstaticas.ObtenerSucursales).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        List<SucursalViewModel> newResponse = JsonConvert.DeserializeObject<List<SucursalViewModel>>(result);
                        newResponse.ForEach(x => idsSucursales.Add(x.Id, x.Id + " - " + x.Sucursal));
                        newResponse.ForEach(x => respuesta.Add(x.Id + " - " + x.Sucursal));

                        response.Dispose();
                    }
                    else
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        string codeError = response.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return respuesta;
            }
            return respuesta;
        }

        /// <summary>
        /// Guardar la información de los productos durante la ejecución
        /// </summary>
        /// <param name="productos"></param>
        /// <returns>Listado de los productos</returns>
        public static async Task<List<string>> VoidGuaradIdProductos(ObservableCollection<ProductosViewModel> productos)
        {
            List<string> respuesta = new List<string>();

            foreach (var producto in productos)
            {
                string nombreProducto = producto.Id + " - " + producto.Nombre;
                respuesta.Add(nombreProducto);
                idsProductos.Add(producto.Id, nombreProducto);
            }

            return respuesta;
        }

        /// <summary>
        /// Agregar una nueva sucursal
        /// </summary>
        /// <param name="sucursal">Objeto con la información de la nueva sucursal</param>
        /// <returns>ServerResponse</returns>
        public static async Task<ServerResponseViewModel> AgregarSucursal(string sucursal)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try {
                using (var client = new HttpClient())
                {
                    SucursalViewModel nuevaSucursal = new SucursalViewModel();
                    nuevaSucursal.Sucursal = sucursal;
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    var response = client.PostAsJsonAsync(VariablesEstaticas.AgregarSucursal, sucursal).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        respuesta = JsonConvert.DeserializeObject<ServerResponseViewModel>(result);
                    }
                    else
                    {
                        respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_CREACION_SUCURSAL);
                        respuesta.Message = Numeratos.Errores.ERROR_CREACION_SUCURSAL.GetDescription()
                                            + " Código --- " + respuesta.Code + " ---";
                        respuesta.Succeddeed = false;
                    }
                }
            }
            catch (Exception en) {
                respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_CREACION_SUCURSAL);
                respuesta.Message = Numeratos.Errores.ERROR_CREACION_SUCURSAL.GetDescription()
                                    + " Código --- " + respuesta.Code + " ---";
                respuesta.Succeddeed = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Obtener la informacipon de los inventarios por sucursal
        /// </summary>
        /// <param name="sucursal">Sucursal</param>
        /// <returns>Listado de los productos en el inventario</returns>
        public static async Task<ObservableCollection<InventarioViewModel>> ObtenerInventarioPorSucursal(string sucursal)
        {
            int idSucursal = 0;
            ObservableCollection<InventarioViewModel> respuesta = new ObservableCollection<InventarioViewModel>();
            idSucursal = idsSucursales.Where(x => x.Value == sucursal).FirstOrDefault().Key;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble((1000000)));

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await client.GetAsync(VariablesEstaticas.ObtenerInventarioSucursal + "?" + VariablesEstaticas.IdSucursal + "=" + idSucursal.ToString() ).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        ObservableCollection<InventarioViewModel> newResponse = JsonConvert.DeserializeObject<ObservableCollection<InventarioViewModel>>(result);
                        idsInventario = newResponse.Select(x => x.Id).ToList();
                        respuesta = newResponse;
                        response.Dispose();
                    }
                    else
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        string codeError = response.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return respuesta;
            }
            return respuesta;
        }

        /// <summary>
        /// Obtener el listado de productos por sucursal y productos
        /// </summary>
        /// <param name="Sucursal">Identificador de la sucursal</param>
        /// <param name="Producto">Identificador del producto</param>
        /// <returns>Listado con la información del  inventario</returns>
        public static async Task<ObservableCollection<InventarioViewModel>> ObtenerProductosPorSucursal(string Sucursal, string Producto)
        {
            int idSucursal = 0;
            int idProducto = 0;
            ObservableCollection<InventarioViewModel> respuesta = new ObservableCollection<InventarioViewModel>();
            idSucursal = idsSucursales.Where(x => x.Value == Sucursal).FirstOrDefault().Key;
            idProducto = idsProductos.Where(x => x.Value == Producto).FirstOrDefault().Key;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble((1000000)));

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await client.GetAsync(VariablesEstaticas.ObtenerProductosSucursal + "?" + VariablesEstaticas.IdProducto + "=" + idProducto.ToString() + "&"
                        + VariablesEstaticas.IdSucursal + "=" + idSucursal.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        ObservableCollection<InventarioViewModel> newResponse = JsonConvert.DeserializeObject<ObservableCollection<InventarioViewModel>>(result);
                        idsInventario = newResponse.Select(x => x.Id).ToList();
                        respuesta = newResponse;
                        response.Dispose();
                    }
                    else
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        string codeError = response.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return respuesta;
            }
            return respuesta;
        }

        /// <summary>
        /// Validar si el nuevo producto existe en el inventario actual
        /// </summary>
        /// <param name="idProductoInventario">Identificador del producto</param>
        /// <returns>bool</returns>
        public static bool ValidarNuevoProductoEnInventario(int idProductoInventario)
        {
            return idsInventario.Any(x => x == idProductoInventario);
        }

        /// <summary>
        /// Agregar la información de un producto al inventario de una sucursal
        /// </summary>
        /// <param name="Producto">Objeto con la información del producto</param>
        /// <param name="Sucursal">Identificador de la sucursal</param>
        /// <param name="producto">Identificador del producto</param>
        /// <returns>ServerResponse</returns>
        public static async Task<ServerResponseViewModel> AgregarProductoAInventario(InventarioViewModel Producto, string Sucursal, string producto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            ProductoInventarioViewModel newProducto = new ProductoInventarioViewModel();
            try
            {
                using (var client = new HttpClient())
                {
                    newProducto.IdProducto = idsProductos.Where(x => x.Value == producto).Select(x => x.Key).FirstOrDefault();
                    newProducto.IdSucursal = idsSucursales.Where(x => x.Value == Sucursal).Select(x => x.Key).FirstOrDefault();
                    newProducto.Precio = Producto.Precio;
                    newProducto.Cantidad = Producto.Cantidad;

                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    var response = client.PostAsJsonAsync(VariablesEstaticas.AgregarProductoInventario, newProducto).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        respuesta = JsonConvert.DeserializeObject<ServerResponseViewModel>(result);
                    }
                    else
                    {
                        respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_PRODUCTO_INVENTARIO);
                        respuesta.Message = Numeratos.Errores.ERROR_PRODUCTO_INVENTARIO.GetDescription()
                                            + " Código --- " + respuesta.Code + " ---";
                        respuesta.Succeddeed = false;
                    }
                }
            }
            catch (Exception en)
            {
                respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_PRODUCTO_INVENTARIO);
                respuesta.Message = Numeratos.Errores.ERROR_PRODUCTO_INVENTARIO.GetDescription()
                                    +" Código --- " + respuesta.Code + " ---";
                respuesta.Succeddeed = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Actualizar la información de un producto en el inventario de una sucursal
        /// </summary>
        /// <param name="Producto">Objeto con la información del producto</param>
        /// <param name="Sucursal">Identificador de la sucursal</param>
        /// <param name="producto">Identificador del producto</param>
        /// <returns>ServerResponse</returns>
        public static async Task<ServerResponseViewModel> ActualizarProductoEnInventario(InventarioViewModel Producto, string sucursal, string producto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            ProductoInventarioViewModel newProducto = new ProductoInventarioViewModel();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    newProducto.IdProducto = idsProductos.Where(x => x.Value == producto).Select(x => x.Key).FirstOrDefault();
                    newProducto.IdSucursal = idsSucursales.Where(x => x.Value == sucursal).Select(x => x.Key).FirstOrDefault();
                    newProducto.Precio = Producto.Precio;
                    newProducto.Cantidad = Producto.Cantidad;

                    var response = client.PutAsJsonAsync(VariablesEstaticas.ActualizarProductoInventario, newProducto).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta.Succeddeed = response.IsSuccessStatusCode;
                        string result = response.Content.ReadAsStringAsync().Result;
                        respuesta = JsonConvert.DeserializeObject<ServerResponseViewModel>(result);
                    }
                    else
                    {
                        respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_ACTUALIZACION_INVENTARIO);
                        respuesta.Message = Numeratos.Errores.ERROR_ACTUALIZACION_INVENTARIO.GetDescription()
                                            + " Código --- " + respuesta.Code + " ---";
                        respuesta.Succeddeed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_ACTUALIZACION_INVENTARIO);
                respuesta.Message = Numeratos.Errores.ERROR_ACTUALIZACION_INVENTARIO.GetDescription()
                                    +" Código --- " + respuesta.Code + " ---";
                respuesta.Succeddeed = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Eliminar la información de un producto de una sucursal
        /// </summary>
        /// <param name="Id">Identificador del producto</param>
        /// <returns>ServerResponse</returns>
        public static async Task<ServerResponseViewModel> EliminarProducto(int Id)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    var response = client.DeleteAsync(VariablesEstaticas.EliminarInventario + "?" + VariablesEstaticas.IdProducto + Id.ToString()).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta.Succeddeed = response.IsSuccessStatusCode;
                        string result = response.Content.ReadAsStringAsync().Result;
                        respuesta = JsonConvert.DeserializeObject<ServerResponseViewModel>(result);
                    }
                    else
                    {
                        respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_ELIMINACION_INVENTARIO);
                        respuesta.Message = Numeratos.Errores.ERROR_ELIMINACION_INVENTARIO.GetDescription()
                                            + " Código --- " + respuesta.Code + " ---";
                        respuesta.Succeddeed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_ELIMINACION_INVENTARIO);
                respuesta.Message = Numeratos.Errores.ERROR_ELIMINACION_INVENTARIO.GetDescription()
                                    +" Código --- " + respuesta.Code + " ---";
                respuesta.Succeddeed = false;
            }
            return respuesta;
        }
    }
}
