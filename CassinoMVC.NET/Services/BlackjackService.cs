using System;
using System.Collections.Generic;
using System.Linq;
using CassinoMVC.Models;

namespace CassinoMVC.Services
{
    /// <summary>
    /// Regras do Blackjack: iniciar, hit, stay e double down.
    /// </summary>
    public static class BlackjackService
    {
        public static BlackjackGame Iniciar(decimal aposta)
        {
            var jogo = new BlackjackGame { ApostaInicial = aposta, ApostaAtual = aposta, Baralho = Baralho.NovoBaralho() };
            jogo.Baralho.Embaralhar();
            // Cartas iniciais
            var c1 = jogo.Baralho.ComprarCarta(); if (c1 != null) jogo.MaoJogador.Add(c1);
            var c2 = jogo.Baralho.ComprarCarta(); if (c2 != null) jogo.MaoJogador.Add(c2);
            var d1 = jogo.Baralho.ComprarCarta(); if (d1 != null) jogo.MaoDealer.Add(d1);
            var d2 = jogo.Baralho.ComprarCarta(); if (d2 != null) jogo.MaoDealer.Add(d2);
            return jogo;
        }
        /// Jogador escolhe comprar mais uma carta.
        public static void Hit(BlackjackGame jogo)
        {
            if (jogo.Estado == BlackjackEstado.Finalizada) return;
            var c = jogo.Baralho.ComprarCarta(); if (c != null) jogo.MaoJogador.Add(c);
            if (Pontos(jogo.MaoJogador) >21) jogo.Estado = BlackjackEstado.Finalizada;
        }
        /// Jogador escolhe dobrar a aposta e receber apenas mais uma carta.
        public static void DoubleDown(BlackjackGame jogo)
        {
            if (jogo.Estado == BlackjackEstado.Finalizada || jogo.DoubleDownUsado) return;
            jogo.ApostaAtual *=2; // dobra a aposta
            jogo.DoubleDownUsado = true;
            var c = jogo.Baralho.ComprarCarta(); if (c != null) jogo.MaoJogador.Add(c); // recebe apenas uma carta
            DealerPlay(jogo);
            jogo.Estado = BlackjackEstado.Finalizada;
        }
        /// Jogador escolhe ficar com as cartas que tem.
        public static void Stay(BlackjackGame jogo)
        {
            if (jogo.Estado == BlackjackEstado.Finalizada) return;
            DealerPlay(jogo);
            jogo.Estado = BlackjackEstado.Finalizada;
        }
        /// Lógica do dealer: parar com minimo de 17.
        private static void DealerPlay(BlackjackGame jogo)
        {
            while (Pontos(jogo.MaoDealer) <17)
            {
                var carta = jogo.Baralho.ComprarCarta();
                if (carta == null) break;
                jogo.MaoDealer.Add(carta);
            }
        }

        public static int Pontos(IEnumerable<Carta> mao)
        {
            int pontos =0; int ases =0;
            foreach (var c in mao)
            {
                pontos += c.Pontuacao;
                if (c.Valor == "A") ases++;
            }
            while (pontos >21 && ases >0)
            {
                pontos -=10; ases--;
            }
            return pontos;
        }

        /// <summary>
        /// Retorna (resultado, multiplicador de retorno total sobre a ApostaAtual)
        /// multiplicador:0 (perde),1 (empate/devolve),2 (ganha).
        /// </summary>
        public static Tuple<string, decimal> Avaliar(BlackjackGame jogo)
        {
            var pontosJogador = Pontos(jogo.MaoJogador);
            var pontosDealer = Pontos(jogo.MaoDealer);
            bool jogadorEstourou = pontosJogador >21;
            bool dealerEstourou = pontosDealer >21;

            if (jogadorEstourou && dealerEstourou) return Tuple.Create("Empate",1m);
            if (jogadorEstourou) return Tuple.Create("Perdeu",0m);
            if (dealerEstourou) return Tuple.Create("Ganhou",2m);
            if (pontosJogador > pontosDealer) return Tuple.Create("Ganhou",2m);
            if (pontosJogador == pontosDealer) return Tuple.Create("Empate",1m);
            return Tuple.Create("Perdeu",0m);
        }
    }
}