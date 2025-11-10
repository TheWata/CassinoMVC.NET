using System;
using System.Collections.Generic;
using System.Linq;
using CassinoMVC.Models;
using CassinoMVC.Data;

namespace CassinoMVC.Controllers
{
    /// <summary>
    /// Controller de Jogador: CRUD simples sobre jogadores em memória.
    /// </summary>
    public class JogadorController
    {
        public List<Jogador> Listar() => Db.Context.Jogadores.ToList();

        public Jogador Criar(string apelido, decimal saldoInicial, string email, int? idUsuario = null)
        {
            var ctx = Db.Context;
            var jogador = new Jogador
            {
                IdJogador = ctx.NextJogadorId(),
                IdUsuario = idUsuario,
                Apelido = apelido,
                Saldo = saldoInicial,
                Email = email,
                DataCriacao = DateTime.UtcNow
            };
            ctx.Jogadores.Add(jogador);
            return jogador;
        }

        public bool AjustarSaldo(int idJogador, decimal delta)
        {
            var j = Db.Context.Jogadores.FirstOrDefault(x => x.IdJogador == idJogador);
            if (j == null) return false;
            j.Saldo += delta;
            return true;
        }

        /// <summary>
        /// Obtém o jogador vinculado a um usuário específico (1:1 opcional).
        /// </summary>
        public Jogador ObterPorUsuarioId(int idUsuario)
            => Db.Context.Jogadores.FirstOrDefault(j => j.IdUsuario == idUsuario);
    }
}