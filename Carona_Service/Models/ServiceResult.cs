using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carona_Service.Models
{
    public class ServiceResult
    {
        public bool Sucesso { get; set; }

        public string Resultado { get; set; }

        public ServiceResult(bool sucesso, string result)
        {
            Sucesso = sucesso;
            Resultado = result;
        }
    }
}
