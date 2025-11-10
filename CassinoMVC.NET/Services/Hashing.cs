using System.Security.Cryptography;
using System.Text;

namespace CassinoMVC.Services
{
    /// <summary>
    /// Serviço utilitário para geração de hash de senha.
    /// Responsável apenas por aplicar SHA256 em uma string de entrada.
    /// </summary>
    public static class Hashing
    {
        /// <summary>
        /// Calcula SHA256 em hexadecimal para o texto informado.
        /// </summary>
        public static string Sha256(string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new System.Text.StringBuilder(bytes.Length *2);
                for (int i =0; i < bytes.Length; i++) sb.Append(bytes[i].ToString("x2"));
                return sb.ToString();
            }
        }
    }
}