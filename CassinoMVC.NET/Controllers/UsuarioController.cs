using CassinoMVC.Models;
using CassinoMVC.Services;

namespace CassinoMVC.Controllers
{
    /// <summary>
    /// Controller de Usuário: camada fina que orquestra services.
    /// Mantém a View desacoplada da lógica de autenticação.
    /// </summary>
    public class UsuarioController
    {
        public Usuario Login(string nomeUsuario, string senha)
            => UsuarioService.Autenticar(nomeUsuario, senha);

        public Usuario CriarUsuario(string nomeCompleto, string nomeUsuario, string senha, CargoUsuario cargo)
            => UsuarioService.CriarUsuario(nomeCompleto, nomeUsuario, senha, cargo);
    }
}