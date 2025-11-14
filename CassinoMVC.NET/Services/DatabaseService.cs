using System.Data.SqlClient;

namespace CassinoMVC.Services
{
    public static class DatabaseService
    {
        
        private static readonly string _connectionString =
            @"Server=UNI-LAB13-001\SQLEXPRESS;Database=ProjetoCassinoMVC;Integrated Security=True;TrustServerCertificate=True;";
        public static SqlConnection GetConnection()
        {
            // O 'using' nos métodos de serviço cuidará de fechar a conexão
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}