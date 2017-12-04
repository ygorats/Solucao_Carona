using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carona_Service.Models
{
    public class CaronaOferta
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }

        public string Descricao { get; set; }

        [Display(Name="Ponto de Partida")]
        [NotMapped]
        public string PontoPartida { get; set; }

        [NotMapped]
        public string PontoChegada { get; set; }

        [NotMapped]
        public string PontosIntermediarios { get; set; }

        [Display(Name = "Horário de partida")]
        [DataType(DataType.Time)]
        public DateTime HorarioPartida { get; set; }

        [Display(Name ="Horário de Chegada")]
        [DataType(DataType.Time)]
        public DateTime HorarioChegada { get; set; }
    }
}
