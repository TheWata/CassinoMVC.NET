using CassinoMVC.Enums;
using CassinoMVC.Models;
using CassinoMVC.Services;

namespace CassinoMVC.Controllers
{
    /// <summary>
    /// Controller de Aposta: abstrai a lógica de registrar diferentes tipos de aposta.
    /// </summary>
    public class ApostaController
    {
        public Aposta ApostarRoleta(int idJogador, decimal valor, TipoApostaRoleta tipo, int? numero = null, CorRoleta? cor = null, bool? parEscolhido = null)
            => ApostaService.RegistrarApostaRoleta(idJogador, valor, tipo, numero, cor, parEscolhido);

        public Aposta ApostarSlots(int idJogador, decimal valor)
            => ApostaService.RegistrarApostaSlots(idJogador, valor);

        public Aposta ApostarBlackjack(int idJogador, decimal valor)
            => ApostaService.RegistrarApostaBlackjack(idJogador, valor);
    }
}