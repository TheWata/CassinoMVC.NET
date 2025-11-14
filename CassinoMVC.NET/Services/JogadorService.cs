using CassinoMVC.Models;
using System;
using System.Data.SqlClient;

namespace CassinoMVC.Services
{
    public class JogadorService
    {
        /// <summary>
        /// Encontra um jogador pelo ID do seu usuário.
        /// </summary>
        public Jogador ObterPorUsuarioId(int idUsuario)
        {
            string sql = "SELECT * FROM Jogadores WHERE IdUsuario = @IdUsuario";

            using (var conn = DatabaseService.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapJogadorFromReader(reader);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Cria uma nova entrada de Jogador ligada a um Usuário.
        /// </summary>
        public Jogador CriarNovoJogadorParaUsuario(int idUsuario, string apelido, decimal saldoInicial)
        {
            var jogador = new Jogador
            {
                IdUsuario = idUsuario,
                Apelido = apelido,
                Saldo = saldoInicial,
                Email = string.Empty, 
                DataCriacao = DateTime.UtcNow
            };

            string sql = @"INSERT INTO Jogadores (IdUsuario, Apelido, Saldo, Email, DataCriacao)
                           VALUES (@IdUsuario, @Apelido, @Saldo, @Email, @DataCriacao);
                           SELECT SCOPE_IDENTITY();";

            using (var conn = DatabaseService.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", jogador.IdUsuario);
                cmd.Parameters.AddWithValue("@Apelido", jogador.Apelido);
                cmd.Parameters.AddWithValue("@Saldo", jogador.Saldo);
                cmd.Parameters.AddWithValue("@Email", jogador.Email);
                cmd.Parameters.AddWithValue("@DataCriacao", jogador.DataCriacao);

                var newId = cmd.ExecuteScalar();
                jogador.IdJogador = Convert.ToInt32(newId);
            }
            return jogador;
        }

        // Método utilitário
        private Jogador MapJogadorFromReader(SqlDataReader reader)
        {
            return new Jogador
            {
                IdJogador = (int)reader["IdJogador"],
                IdUsuario = (int)reader["IdUsuario"],
                Apelido = (string)reader["Apelido"],
                Saldo = (decimal)reader["Saldo"],
                Email = (string)reader["Email"],
                DataCriacao = (DateTime)reader["DataCriacao"]
            };
        }
    }
}