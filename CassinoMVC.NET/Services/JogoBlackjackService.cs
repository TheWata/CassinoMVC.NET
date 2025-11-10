using System.Collections.Generic;
using CassinoMVC.Models;

namespace CassinoMVC.Services
{
    /// <summary>
    /// Regras utilitárias para o jogo de Blackjack (21): criação de baralho, embaralhamento e cálculo de pontuação.
    /// </summary>
    public static class JogoBlackjackService
    {
        /// <summary>
        /// Cria um baralho padrão (52 cartas) e embaralha.
        /// </summary>
        public static Baralho CriarBaralhoEmbaralhado()
        {
            var b = Baralho.NovoBaralho();
            b.Embaralhar();
            return b;
        }

        /// <summary>
        /// Calcula a pontuação de uma mão tratando ases como 11 inicialmente e ajustando para 1 se estourar (>21).
        /// </summary>
        public static int PontosDaMao(IEnumerable<Carta> mao)
        {
            int pontos = 0;
            int ases = 0;
            foreach (var c in mao)
            {
                pontos += c.Pontuacao;
                if (c.Valor == "A") ases++;
            }
            // Ajuste dinâmico dos ases de 11 para 1 conforme necessidade.
            while (pontos > 21 && ases > 0)
            {
                pontos -= 10;
                ases--;
            }
            return pontos;
        }
    }
}