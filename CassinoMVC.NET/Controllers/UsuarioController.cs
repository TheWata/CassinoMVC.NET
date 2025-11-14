using CassinoMVC.Models;
using CassinoMVC.Services;

namespace CassinoMVC.Controllers
{
    public class UsuarioController
    {
        // Instancia o NOVO serviço que usa ADO.NET
        private readonly UsuarioService _usuarioService = new UsuarioService();

        public Usuario Login(string nomeUsuario, string senha)
            => _usuarioService.Autenticar(nomeUsuario, senha); // Chama o método de banco

        public Usuario CriarUsuario(string nomeCompleto, string nomeUsuario, string senha, CargoUsuario cargo)
            => _usuarioService.CriarUsuario(nomeCompleto, nomeUsuario, senha, cargo); // Chama o método de banco
    }
}