using System;
using System.Collections.Generic;
using System.Text;

namespace Cliente_WPF.Models
{
    public class ServerResponseViewModel
    {
        /// <summary>
        /// Objeto para generar las respuestas de la aplicación
        /// </summary>
        public int Id { get; set; }
        public string Message { get; set; }
        public string Header { get; set; }
        public bool Succeddeed { get; set; }
        public int Code { get; set; }
        public ServerResponseViewModel() 
        {
            Id = 0;
            Message = string.Empty;
            Header = string.Empty;
            Succeddeed = false;
            Code = 0;
        }
    }
}
