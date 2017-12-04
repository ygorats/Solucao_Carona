using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carona_Service.Models
{
    public class Veiculo
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }

        public string Placa { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }
    }
}
