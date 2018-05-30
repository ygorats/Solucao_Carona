using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Carona_Service.Models
{
    public class CaronaBusca
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }

        public string Descricao { get; set; }

        [Display(Name="Ponto de Partida")]
        [NotMapped]
        public Position PontoPartida { get; set; }

        [NotMapped]
        public Position PontoChegada { get; set; }

        [Display(Name = "Horário de partida")]
        [DataType(DataType.Time)]
        public DateTime HorarioPartida { get; set; }

        [Display(Name ="Horário de Chegada")]
        [DataType(DataType.Time)]
        public DateTime HorarioChegada { get; set; }

        
    }
}
