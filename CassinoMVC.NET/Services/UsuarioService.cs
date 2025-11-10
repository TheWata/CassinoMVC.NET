using System.Linq;
using CassinoMVC.Models;
using CassinoMVC.Data;

namespace CassinoMVC.Services
{
    /// <summary>
    /// Regras de negócio relacionadas a usuários do sistema.
    /// Inclui autenticação e criação de usuários com senha hasheada.
    /// </summary>
    public static class UsuarioService
    {
        /// <summary>
        /// Gera o hash da senha usando SHA256 (ver Hashing service).
        /// </summary>
        public static string GerarHashSenha(string senha) => Hashing.Sha256(senha);

        /// <summary>
        /// Autentica um usuário comparando o hash da senha informada
        /// com o hash armazenado no contexto em memória.
        /// </summary>
        public static Usuario Autenticar(string nomeUsuario, string senha)
        {
            var hash = GerarHashSenha(senha);
            return Db.Context.Usuarios.FirstOrDefault(u => u.NomeUsuario == nomeUsuario && u.SenhaHash == hash);
        }

        /// <summary>
        /// Cria um novo usuário no contexto em memória.
        /// Define o próximo Id, aplica hash na senha e persiste em memória.
        /// </summary>
        public static Usuario CriarUsuario(string nomeCompleto, string nomeUsuario, string senha, CargoUsuario cargo)
        {
            var ctx = Db.Context;
            var usuario = new Usuario
            {
                IdUsuario = ctx.NextUsuarioId(),
                NomeCompleto = nomeCompleto,
                NomeUsuario = nomeUsuario,
                SenhaHash = GerarHashSenha(senha),
                Cargo = cargo,
                DataCriacao = System.DateTime.UtcNow
            };
            ctx.Usuarios.Add(usuario);
            return usuario;
        }
    }
}