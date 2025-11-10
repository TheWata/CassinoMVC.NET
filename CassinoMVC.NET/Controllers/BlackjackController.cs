using CassinoMVC.Models;
using CassinoMVC.Services;

namespace CassinoMVC.Controllers
{
    /// <summary>
    /// Controller MVC para orquestrar o fluxo do Blackjack a partir da UI.
    /// </summary>
    public class BlackjackController
    {
        public BlackjackGame Iniciar(decimal aposta) => BlackjackService.Iniciar(aposta);
        public void Hit(BlackjackGame jogo) => BlackjackService.Hit(jogo);
        public void Stay(BlackjackGame jogo) => BlackjackService.Stay(jogo);
        public void DoubleDown(BlackjackGame jogo) => BlackjackService.DoubleDown(jogo);
        public System.Tuple<string, decimal> Avaliar(BlackjackGame jogo) => BlackjackService.Avaliar(jogo);
        public int PontosJogador(BlackjackGame jogo) => BlackjackService.Pontos(jogo.MaoJogador);
        public int PontosDealer(BlackjackGame jogo) => BlackjackService.Pontos(jogo.MaoDealer);
    }
}