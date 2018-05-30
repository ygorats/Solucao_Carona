using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms.Maps;

namespace Carona_Service.Models
{
    public class CaronaOferta
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }

        public string Descricao { get; set; }

        [Display(Name="Ponto de Partida")]
        [NotMapped]
        public Position PontoPartida { get; set; }

        [NotMapped]
        public Position PontoChegada { get; set; }

        [NotMapped]
        public List<Position> PontosIntermediarios { get; set; }

        [Display(Name = "Horário de partida")]
        [DataType(DataType.Time)]
        public DateTime HorarioPartida { get; set; }

        [Display(Name ="Horário de Chegada")]
        [DataType(DataType.Time)]
        public DateTime HorarioChegada { get; set; }
    }
}
