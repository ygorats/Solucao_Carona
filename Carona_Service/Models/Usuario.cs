using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carona_Service.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(11), MinLength(11)]
        public string Cpf { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }
    }
}
