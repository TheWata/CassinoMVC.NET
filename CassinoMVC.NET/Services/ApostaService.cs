using System;
using System.Collections.Generic;
using CassinoMVC.Enums;
using CassinoMVC.Models;
using CassinoMVC.Data;
using CassinoMVC.Utils;
using System.Linq;

namespace CassinoMVC.Services
{
    public static class ApostaService
    {
      
        private static Tuple<DataContext, Jogador, TipoJogo> ValidarBase(int idJogador, decimal valor, string nomeTipoJogo)
        {
            var ctx = new DataContext();
            var jogador = ctx.Jogadores.Find(idJogador);

            if (jogador == null) throw new Exception("Jogador não encontrado");
            if (jogador.Saldo < valor) throw new Exception("Saldo insuficiente");

            var tipoJogo = ctx.TiposJogo.FirstOrDefault(t => t.Nome == nomeTipoJogo);

            if (tipoJogo == null) throw new Exception("Tipo de jogo " + nomeTipoJogo + " não cadastrado");

            return new Tuple<DataContext, Jogador, TipoJogo>(ctx, jogador, tipoJogo);
        }


        public static Aposta RegistrarApostaRoleta(int idJogador, decimal valor, TipoApostaRoleta tipo, int? numero, CorRoleta? cor, bool? parEscolhido)
        {
            var baseInfo = ValidarBase(idJogador, valor, Constants.GameNames.Roleta);

            using (var ctx = baseInfo.Item1)
            {
                var jogador = baseInfo.Item2;
                var tipoJogo = baseInfo.Item3;

                var sessao = new SessaoJogo();
                sessao.IdTipoJogo = tipoJogo.IdTipoJogo;
                sessao.DataInicio = DateTime.UtcNow;
               

                var rodada = JogoRoletaService.ExecutarRodada(tipo, numero, cor, parEscolhido);
                sessao.DataFim = DateTime.UtcNow;
                sessao.Resultado = "Número: " + rodada.NumeroSorteado + " Cor: " + rodada.Cor;

                var avaliacao = JogoRoletaService.AvaliarAposta(rodada, tipo, numero, cor, parEscolhido);
                var ganhou = avaliacao.ganhou;
                var multiplicador = avaliacao.multiplicador;
                decimal premio = ganhou ? valor * multiplicador : 0m;

                jogador.Saldo -= valor;
                jogador.Saldo += premio;

                var aposta = new Aposta();
                aposta.IdJogador = jogador.IdJogador;

              
                aposta.Sessao = sessao;

                aposta.ValorApostado = valor;
                aposta.Resultado = ganhou ? "Ganhou" : "Perdeu";
                aposta.ValorPremio = premio;
                aposta.DataAposta = DateTime.UtcNow;

                ctx.Apostas.Add(aposta); 
                ctx.SaveChanges();

                return aposta;
            }
        }


        public static Aposta RegistrarApostaSlots(int idJogador, decimal valor)
        {
            var baseInfo = ValidarBase(idJogador, valor, Constants.GameNames.Slots);

            using (var ctx = baseInfo.Item1)
            {
                var jogador = baseInfo.Item2;
                var tipoJogo = baseInfo.Item3;

                var sessao = new SessaoJogo();
                sessao.IdTipoJogo = tipoJogo.IdTipoJogo;
                sessao.DataInicio = DateTime.UtcNow;
               
                var slot = JogoSlotService.Girar();
                sessao.DataFim = DateTime.UtcNow;
                sessao.Resultado = slot.Resultado;

                decimal premio = valor * slot.MultiplicadorGanho;
                jogador.Saldo -= valor;
                jogador.Saldo += premio;
                bool ganhou = slot.MultiplicadorGanho > 0m;

                var aposta = new Aposta();
                aposta.IdJogador = jogador.IdJogador;

                
                aposta.Sessao = sessao; 

                aposta.ValorApostado = valor;
                aposta.Resultado = ganhou ? "Ganhou" : "Perdeu";
                aposta.ValorPremio = premio;
                aposta.DataAposta = DateTime.UtcNow;

                ctx.Apostas.Add(aposta);
                ctx.SaveChanges();

                return aposta;
            }
        }


        public static Aposta RegistrarApostaBlackjack(int idJogador, decimal valor)
        {
            var baseInfo = ValidarBase(idJogador, valor, Constants.GameNames.Blackjack);

            using (var ctx = baseInfo.Item1)
            {
                var jogador = baseInfo.Item2;
                var tipoJogo = baseInfo.Item3;

                var sessao = new SessaoJogo();
                sessao.IdTipoJogo = tipoJogo.IdTipoJogo;
                sessao.DataInicio = DateTime.UtcNow;


                // ... (Lógica do jogo Blackjack) ...
                var baralho = JogoBlackjackService.CriarBaralhoEmbaralhado();
                var maoJogador = new List<Carta>();
                var c1 = baralho.ComprarCarta(); if (c1 != null) maoJogador.Add(c1);
                var c2 = baralho.ComprarCarta(); if (c2 != null) maoJogador.Add(c2);
                var maoDealer = new List<Carta>();
                var d1 = baralho.ComprarCarta(); if (d1 != null) maoDealer.Add(d1);
                var d2 = baralho.ComprarCarta(); if (d2 != null) maoDealer.Add(d2);

                while (JogoBlackjackService.PontosDaMao(maoDealer) < 17)
                {
                    var c = baralho.ComprarCarta(); if (c == null) break;
                    maoDealer.Add(c);
                }
                int pj = JogoBlackjackService.PontosDaMao(maoJogador);
                int pd = JogoBlackjackService.PontosDaMao(maoDealer);
                bool estJ = pj > 21; bool estD = pd > 21;

                string resultado; decimal premio;
                if (estJ && estD) { resultado = "Empate"; premio = valor; }
                else if (estJ) { resultado = "Perdeu"; premio = 0m; }
                else if (estD || pj > pd) { resultado = "Ganhou"; premio = valor * 2m; }
                else if (pj == pd) { resultado = "Empate"; premio = valor; }
                else { resultado = "Perdeu"; premio = 0m; }
                // ... (Fim da lógica do jogo) ...

                jogador.Saldo -= valor;
                jogador.Saldo += premio;
                sessao.DataFim = DateTime.UtcNow;
                sessao.Resultado = "Jogador: " + pj + " Dealer: " + pd;

                var aposta = new Aposta();
                aposta.IdJogador = jogador.IdJogador;


                aposta.Sessao = sessao;
                aposta.ValorApostado = valor;
                aposta.Resultado = resultado;
                aposta.ValorPremio = premio - valor > 0 ? premio - valor : 0;
                aposta.DataAposta = DateTime.UtcNow;

                ctx.Apostas.Add(aposta);
                ctx.SaveChanges();

                return aposta;
            }
        }
    }
}