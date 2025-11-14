using System;
using System.Collections.Generic;
using CassinoMVC.Enums;
using CassinoMVC.Models;
using CassinoMVC.Data;
using CassinoMVC.Utils;
using System.Linq; // <-- ADICIONADO

namespace CassinoMVC.Services
{
    /// <summary>
    /// Service de apostas: regras de persistência + aplicação de saldo.
    /// Mantém métodos simples delegando regras específicas para serviços de jogo.
    /// </summary>
    public static class ApostaService
    {
        // Validação comum (DRY)
        private static Tuple<DataContext, Jogador, TipoJogo> ValidarBase(int idJogador, decimal valor, string nomeTipoJogo)
        {
            // Lógica ANTIGA:
            // var ctx = Db.Context;
            // Lógica NOVA:
            var ctx = new DataContext(); // Criado aqui, mas será retornado

            // Lógica ANTIGA:
            // var jogador = ctx.Jogadores.Find(j => j.IdJogador == idJogador);
            // Lógica NOVA:
            var jogador = ctx.Jogadores.Find(idJogador); // .Find(PK)

            if (jogador == null) throw new Exception("Jogador não encontrado");
            if (jogador.Saldo < valor) throw new Exception("Saldo insuficiente");

            // Lógica ANTIGA:
            // var tipoJogo = ctx.TiposJogo.Find(t => t.Nome == nomeTipoJogo);
            // Lógica NOVA:
            var tipoJogo = ctx.TiposJogo.FirstOrDefault(t => t.Nome == nomeTipoJogo); // .Find não funciona em colunas não-PK

            if (tipoJogo == null) throw new Exception("Tipo de jogo " + nomeTipoJogo + " não cadastrado");

            // Retorna o contexto para ser usado (e descartado) pelo chamador
            return new Tuple<DataContext, Jogador, TipoJogo>(ctx, jogador, tipoJogo);
        }

        /// <summary>
        /// Aposta na Roleta (internas ou externas). Lógica de sorteio/avaliação fica em JogoRoletaService.
        /// </summary>
        public static Aposta RegistrarApostaRoleta(int idJogador, decimal valor, TipoApostaRoleta tipo, int? numero, CorRoleta? cor, bool? parEscolhido)
        {
            var baseInfo = ValidarBase(idJogador, valor, Constants.GameNames.Roleta);

            // Lógica NOVA: O chamador (este método) é responsável por descartar o Ctx
            using (var ctx = baseInfo.Item1)
            {
                var jogador = baseInfo.Item2;
                var tipoJogo = baseInfo.Item3;

                var sessao = new SessaoJogo();
                // sessao.IdSessao = ctx.NextSessaoId(); // <-- REMOVIDO
                sessao.IdTipoJogo = tipoJogo.IdTipoJogo;
                sessao.DataInicio = DateTime.UtcNow;
                ctx.Sessoes.Add(sessao);

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
                // aposta.IdAposta = ctx.NextApostaId(); // <-- REMOVIDO
                aposta.IdJogador = jogador.IdJogador;
                aposta.IdSessao = sessao.IdSessao;
                aposta.ValorApostado = valor;
                aposta.Resultado = ganhou ? "Ganhou" : "Perdeu";
                aposta.ValorPremio = premio;
                aposta.DataAposta = DateTime.UtcNow;

                ctx.Apostas.Add(aposta);

                ctx.SaveChanges(); // <-- ADICIONADO (Salva Sessao, Aposta e Saldo do Jogador)

                return aposta;
            }
        }

        /// <summary>
        /// Aposta nos Slots (giro único). Regras de geração/multiplicador em JogoSlotService.
        /// </summary>
        public static Aposta RegistrarApostaSlots(int idJogador, decimal valor)
        {
            var baseInfo = ValidarBase(idJogador, valor, Constants.GameNames.Slots);

            // Lógica NOVA:
            using (var ctx = baseInfo.Item1)
            {
                var jogador = baseInfo.Item2;
                var tipoJogo = baseInfo.Item3;

                var sessao = new SessaoJogo();
                // sessao.IdSessao = ctx.NextSessaoId(); // <-- REMOVIDO
                sessao.IdTipoJogo = tipoJogo.IdTipoJogo;
                sessao.DataInicio = DateTime.UtcNow;
                ctx.Sessoes.Add(sessao);

                var slot = JogoSlotService.Girar();
                sessao.DataFim = DateTime.UtcNow;
                sessao.Resultado = slot.Resultado;

                decimal premio = valor * slot.MultiplicadorGanho;
                jogador.Saldo -= valor;
                jogador.Saldo += premio;
                bool ganhou = slot.MultiplicadorGanho > 0m;

                var aposta = new Aposta();
                // aposta.IdAposta = ctx.NextApostaId(); // <-- REMOVIDO
                aposta.IdJogador = jogador.IdJogador;
                aposta.IdSessao = sessao.IdSessao;
                aposta.ValorApostado = valor;
                aposta.Resultado = ganhou ? "Ganhou" : "Perdeu";
                aposta.ValorPremio = premio;
                aposta.DataAposta = DateTime.UtcNow;

                ctx.Apostas.Add(aposta);

                ctx.SaveChanges(); // <-- ADICIONADO

                return aposta;
            }
        }

        /// <summary>
        /// Aposta simplificada de Blackjack (uma rodada auto-resolvida).
        /// </summary>
        public static Aposta RegistrarApostaBlackjack(int idJogador, decimal valor)
        {
            var baseInfo = ValidarBase(idJogador, valor, Constants.GameNames.Blackjack);

            // Lógica NOVA:
            using (var ctx = baseInfo.Item1)
            {
                var jogador = baseInfo.Item2;
                var tipoJogo = baseInfo.Item3;

                var sessao = new SessaoJogo();
                // sessao.IdSessao = ctx.NextSessaoId(); // <-- REMOVIDO
                sessao.IdTipoJogo = tipoJogo.IdTipoJogo;
                sessao.DataInicio = DateTime.UtcNow;
                ctx.Sessoes.Add(sessao);

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

                jogador.Saldo -= valor;
                jogador.Saldo += premio;
                sessao.DataFim = DateTime.UtcNow;
                sessao.Resultado = "Jogador: " + pj + " Dealer: " + pd;

                var aposta = new Aposta();
                // aposta.IdAposta = ctx.NextApostaId(); // <-- REMOVIDO
                aposta.IdJogador = jogador.IdJogador;
                aposta.IdSessao = sessao.IdSessao;
                aposta.ValorApostado = valor;
                aposta.Resultado = resultado;
                aposta.ValorPremio = premio - valor > 0 ? premio - valor : 0;
                aposta.DataAposta = DateTime.UtcNow;

                ctx.Apostas.Add(aposta);

                ctx.SaveChanges(); // <-- ADICIONADO

                return aposta;
            }
        }
    }
}