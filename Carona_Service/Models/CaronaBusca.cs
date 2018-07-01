using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
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
        public TimeSpan HorarioPartida { get; set; }

        [Display(Name ="Horário de Chegada")]
        [DataType(DataType.Time)]
        public TimeSpan HorarioChegada { get; set; }

        
    }
}
