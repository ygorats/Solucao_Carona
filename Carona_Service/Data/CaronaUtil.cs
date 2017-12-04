using Carona_Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carona_Service.Data
{
    public class CaronaUtil
    {
        private static string _pontoOrigem;
        private static string _pontoDestino;
        private static string _pontosIntermediarios = "";

        public static async Task<int> CadastreCaronaBuscaAsync(CaronaBusca carona, Carona_ServiceContext contexto)
        {
            DefinePontos(carona.PontoPartida, carona.PontoChegada);
            var origem = string.Concat("geography::STPointFromText('POINT(", _pontoOrigem, ")', 4985), ");
            var destino = string.Concat("geography::STPointFromText('POINT(", _pontoDestino, ")', 4985), ");
            var trajeto = string.Concat("geography::STMPointFromText('MULTIPOINT(", _pontoOrigem, ", ", _pontoDestino, ")', 4985), ");

            var sql = new StringBuilder();

            sql.Append("INSERT INTO CARONABUSCA (ID, IDUSUARIO, DESCRICAO, PONTOPARTIDA, PONTOCHEGADA, TRAJETO, HORARIOPARTIDA, HORARIOCHEGADA) ");
            sql.Append("VALUES (@id, @idUsuario, @descricao, ");

            sql.Append(origem);
            sql.Append(destino);
            sql.Append(trajeto);
            sql.Append(" cast(@horarioPartida as time), cast(@horarioChegada as time)) ");

            int resultado = -1;

            try
            {
                resultado = await contexto.Database.ExecuteSqlCommandAsync(new RawSqlString(sql.ToString()),
                                                                            new SqlParameter("@id", carona.Id.ToString()),
                                                                            new SqlParameter("@idUsuario", carona.IdUsuario.ToString()),
                                                                            new SqlParameter("@descricao", carona.Descricao),
                                                                            new SqlParameter("@horarioPartida", carona.HorarioPartida.ToShortTimeString()),
                                                                            new SqlParameter("@horarioChegada", carona.HorarioChegada.ToShortTimeString()));
               

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }

            return resultado;
        }

        public static async Task<int> CadastreCaronaOfertaAsync(CaronaOferta carona, Carona_ServiceContext contexto)
        {
            DefinePontos(carona.PontoPartida, carona.PontoChegada, carona.PontosIntermediarios);

            var origem = string.Concat("geography::STPointFromText('POINT(", _pontoOrigem, ")', 4985), ");
            var destino = string.Concat("geography::STPointFromText('POINT(", _pontoDestino, ")', 4985), ");
            var trajeto = string.Concat("geography::STLineFromText('LINESTRING(", _pontoOrigem, ", ", _pontosIntermediarios, ", ", _pontoDestino, ")', 4985), ");


            var sql = new StringBuilder();

            sql.Append("INSERT INTO CARONAOFERTA (ID, IDUSUARIO, DESCRICAO, PONTOPARTIDA, PONTOCHEGADA, TRAJETO, HORARIOPARTIDA, HORARIOCHEGADA) ");
            sql.Append("VALUES (@id, @idUsuario, @descricao, ");

            sql.Append(origem);
            sql.Append(destino);
            sql.Append(trajeto);
            sql.Append(" cast(@horarioPartida as time), cast(@horarioChegada as time)) ");

            int resultado = -1;

            try
            {
                resultado = await contexto.Database.ExecuteSqlCommandAsync(new RawSqlString(sql.ToString()),
                                                                            new SqlParameter("@id", carona.Id.ToString()),
                                                                            new SqlParameter("@idUsuario", carona.IdUsuario.ToString()),
                                                                            new SqlParameter("@descricao", carona.Descricao),
                                                                            new SqlParameter("@horarioPartida", carona.HorarioPartida.ToShortTimeString()),
                                                                            new SqlParameter("@horarioChegada", carona.HorarioChegada.ToShortTimeString()));

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }

            return resultado;
        }

        public static async Task<List<CaronaOferta>> ConsulteCaronasOfertadasAsync(string id, Carona_ServiceContext contexto)
        {
            var consulta = new StringBuilder();
            consulta.Append(" SELECT ");
            consulta.Append("   OFERTA.ID, OFERTA.IDUSUARIO, OFERTA.DESCRICAO, OFERTA.HORARIOPARTIDA, OFERTA.HORARIOCHEGADA ");
            consulta.Append(" FROM CARONAOFERTA OFERTA, CARONABUSCA BUSCA ");
            consulta.Append(" WHERE ");
            consulta.Append("   BUSCA.PONTOPARTIDA.STDistance(OFERTA.TRAJETO) < 1001 AND  ");
            consulta.Append("   BUSCA.PONTOCHEGADA.STDistance(OFERTA.TRAJETO) < 1001 AND ");
            consulta.Append("   (DATEDIFF(MINUTE, BUSCA.HORARIOCHEGADA, OFERTA.HORARIOCHEGADA) BETWEEN -30 AND 30) AND ");
            consulta.Append("   ((BUSCA.PONTOPARTIDA.STDistance(OFERTA.PONTOPARTIDA) + BUSCA.PONTOCHEGADA.STDistance(OFERTA.PONTOCHEGADA)) > OFERTA.PONTOPARTIDA.STDistance(OFERTA.PONTOCHEGADA)) AND ");
            consulta.Append("   BUSCA.ID = '" + id + "' ");

            var resultado = await contexto.CaronaOferta.FromSql(consulta.ToString()).ToListAsync();

            return resultado;
        }

        private static void DefinePontos(string origem, string destino, string intermediarios = "")
        {
            _pontoOrigem = origem.Split(" ").Reverse().Join(" ");
            _pontoDestino = destino.Split(" ").Reverse().Join(" ");

            if (!string.IsNullOrEmpty(intermediarios))
            {
                var pontos = intermediarios.Split(",").ToList();    //Separa os pontos intermediarios

                pontos.ForEach(x => x = x.Split(" ").Reverse().Join(" "));  //Inverte as coordenadas

                intermediarios = pontos.Join(", ");     //Junta os pontos de volta

                _pontosIntermediarios = intermediarios;
            }
        }
    }
}
