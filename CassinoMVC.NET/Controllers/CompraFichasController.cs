using System;
using CassinoMVC.Data;
using CassinoMVC.Models;
using System.Linq; // Adicionado para FirstOrDefault (embora trocado por Find)

namespace CassinoMVC.Controllers
{
    /// <summary>
    /// Controller para compras de fichas (popup simples na HomePage).
    /// Registra uma "aposta" especial com TipoDescricao = "Compra de fichas".
    /// </summary>
    public class CompraFichasController
    {
        public bool Comprar(int idJogador, decimal valor)
        {
            if (valor <= 0) return false;

            // Lógica ANTIGA:
            // var ctx = Db.Context;
            // var jogador = System.Linq.Enumerable.FirstOrDefault(ctx.Jogadores, j => j.IdJogador == idJogador);

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                // Usa .Find() que é otimizado para busca por Chave Primária
                var jogador = ctx.Jogadores.Find(idJogador);
                if (jogador == null) return false;

                // Credita saldo
                jogador.Saldo += valor;

                // Registra como "aposta" para aparecer no relatório
                var compra = new Aposta();
                // compra.IdAposta = ctx.NextApostaId(); // <-- REMOVIDO
                compra.IdJogador = jogador.IdJogador;
                compra.IdSessao = 0; // sem sessão de jogo
                compra.ValorApostado = valor;
                compra.Resultado = "Fichas Compradas";
                compra.ValorPremio = 0m;
                compra.DataAposta = System.DateTime.UtcNow;
                compra.TipoDescricao = "Compra de fichas";

                ctx.Apostas.Add(compra);

                // Salva TODAS as alterações (Saldo do Jogador E nova Aposta) no banco
                ctx.SaveChanges();
                return true;
            }
        }
    }
}