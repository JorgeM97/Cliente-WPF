using Cliente_WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cliente_WPF.Helpers;
using System.Linq;

namespace Cliente_WPF.Services
{
    public class ProductosLogic
    {
        /// <summary>
        /// Lista con los Identificadores de los productos
        /// </summary>
        public static List<int> idsProductos;

        /// <summary>
        /// Obtener la información de los productos registrados
        /// </summary>
        /// <returns>Objeto con la información de los productos</returns>
        public static async Task<ObservableCollection<ProductosViewModel>> ObtenerProductos()
        {
            ObservableCollection<ProductosViewModel> respuesta = new ObservableCollection<ProductosViewModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble((1000000)));

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await client.GetAsync(VariablesEstaticas.ObtenerProductos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        ObservableCollection<ProductosViewModel> newResponse = JsonConvert.DeserializeObject<ObservableCollection<ProductosViewModel>>(result);
                        idsProductos = newResponse.Select(x => x.Id).ToList();
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
        /// Agregar un nuevo producto
        /// </summary>
        /// <param name="Producto">Objeto con la información del producto</param>
        /// <returns>ServerResponse</returns>
        public static async Task<ServerResponseViewModel> AgregarProducto(ProductosViewModel Producto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    var response = client.PostAsJsonAsync(VariablesEstaticas.AgregarProducto, Producto).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        respuesta = JsonConvert.DeserializeObject<ServerResponseViewModel>(result);
                    }
                    else
                    {
                        respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_CREACION_PRODUCTO);
                        respuesta.Message = Numeratos.Errores.ERROR_CREACION_PRODUCTO.GetDescription()
                                            + " Código --- " + respuesta.Code + " ---";
                        respuesta.Succeddeed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_CREACION_PRODUCTO);
                respuesta.Message = Numeratos.Errores.ERROR_CREACION_PRODUCTO.GetDescription()
                                    + " Código --- " + respuesta.Code + " ---";
                respuesta.Succeddeed = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Actualizar la información de un producto
        /// </summary>
        /// <param name="Producto">Objeto con la información del producto</param>
        /// <returns>ServerResponse</returns>
        public static async Task<ServerResponseViewModel> ActualizarProducto(ProductosViewModel Producto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(VariablesEstaticas.CadenaConexionAPI);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(VariablesEstaticas.TipoRespuestaJson));

                    var response = client.PutAsJsonAsync(VariablesEstaticas.ActualizarProducto, Producto).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta.Succeddeed = response.IsSuccessStatusCode;
                        string result = response.Content.ReadAsStringAsync().Result;
                        respuesta = JsonConvert.DeserializeObject<ServerResponseViewModel>(result);
                    }
                    else
                    {
                        respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_ACTUALIZACION_PRODUCTO);
                        respuesta.Message = Numeratos.Errores.ERROR_ACTUALIZACION_PRODUCTO.GetDescription()
                                            + " Código --- " + respuesta.Code + " ---";
                        respuesta.Succeddeed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Code = Convert.ToInt32(Numeratos.Errores.ERROR_ACTUALIZACION_PRODUCTO);
                respuesta.Message = Numeratos.Errores.ERROR_ACTUALIZACION_PRODUCTO.GetDescription()
                                    + " Código --- " + respuesta.Code + " ---";
                respuesta.Succeddeed = false;
            }

            return respuesta;
        }

        /// <summary>
        /// Validar si un producto es nuevo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool ValidarNuevoProducto(int Id)
        {
            return idsProductos.Any(x => x == Id);
        }
    }
}
