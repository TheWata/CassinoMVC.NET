using System.Linq;
using CassinoMVC.Models;
using CassinoMVC.Data;

namespace CassinoMVC.Services
{
    /// <summary>
    /// Regras de negócio relacionadas a usuários do sistema.
    /// (AGORA É UMA CLASSE DE INSTÂNCIA)
    /// </summary>
    public class UsuarioService // <-- 'static' REMOVIDO
    {
        /// <summary>
        /// Gera o hash da senha usando SHA256 (ver Hashing service).
        /// </summary>
        public string GerarHashSenha(string senha) => Hashing.Sha256(senha); // <-- 'static' REMOVIDO

        /// <summary>
        /// Autentica um usuário comparando o hash da senha informada
        /// com o hash armazenado no banco de dados via EF.
        /// </summary>
        public Usuario Autenticar(string nomeUsuario, string senha) // <-- 'static' REMOVIDO
        {
            var hash = GerarHashSenha(senha);

            using (var ctx = new DataContext())
            {
                return ctx.Usuarios.FirstOrDefault(u => u.NomeUsuario == nomeUsuario && u.SenhaHash == hash);
            }
        }

        /// <summary>
        /// Cria um novo usuário no banco de dados via EF.
        /// </summary>
        public Usuario CriarUsuario(string nomeCompleto, string nomeUsuario, string senha, CargoUsuario cargo) // <-- 'static' REMOVIDO
        {
            var usuario = new Usuario
            {
                NomeCompleto = nomeCompleto,
                NomeUsuario = nomeUsuario,
                SenhaHash = GerarHashSenha(senha),
                Cargo = cargo,
                DataCriacao = System.DateTime.UtcNow
            };

            using (var ctx = new DataContext())
            {
                ctx.Usuarios.Add(usuario);
                ctx.SaveChanges();
            }
            return usuario;
        }
    }
}