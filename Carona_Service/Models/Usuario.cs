using Carona_Service.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Carona_Service.Models
{
    [JsonObject]
    public class Usuario
    {
        //public Guid Id { get; set; }
        //[EmailAddress]
        //public string Email { get; set; }

        public Guid Id { get { return Id; } set => Id = Guid.NewGuid(); }

        [JsonProperty("id")]
        public string GoogleId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("verified_email")]
        public bool EmailVerificado { get; set; }

        [JsonProperty("name")]
        public string Nome { get; set; }

        [MaxLength(11), MinLength(11)]
        public string Cpf { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string UrlFoto { get; set; }

        [NotMapped]
        public Image Foto
        {
            get { return Foto; }
            set => Foto = new Image() { Source = ImageSource.FromUri(new Uri(UrlFoto)) };
        }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [NotMapped]
        public EnumSexo Sexo => Dicionarios.DicionarioSexo[Gender];
    }
}
