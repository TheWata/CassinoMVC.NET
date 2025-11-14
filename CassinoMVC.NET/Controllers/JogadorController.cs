using System;
using System.Collections.Generic;
using System.Linq;
using CassinoMVC.Models;
using CassinoMVC.Data;

namespace CassinoMVC.Controllers
{
    /// <summary>
    /// Controller de Jogador: CRUD simples sobre jogadores.
    /// </summary>
    public class JogadorController
    {
        public List<Jogador> Listar()
        {
            // Lógica ANTIGA: Db.Context.Jogadores.ToList();
            using (var ctx = new DataContext())
            {
                return ctx.Jogadores.ToList();
            }
        }

        public Jogador Criar(string apelido, decimal saldoInicial, string email, int? idUsuario = null)
        {
            // Lógica ANTIGA:
            // var ctx = Db.Context;
            // var jogador = new Jogador { ... };
            // jogador.IdJogador = ctx.NextJogadorId(); // <-- Será removido
            // ctx.Jogadores.Add(jogador);
            // return jogador;

            // Lógica NOVA:
            var jogador = new Jogador
            {
                // IdJogador (PK) será definido pelo SQL Server
                IdUsuario = idUsuario,
                Apelido = apelido,
                Saldo = saldoInicial,
                Email = email,
                DataCriacao = DateTime.UtcNow
            };

            using (var ctx = new DataContext())
            {
                ctx.Jogadores.Add(jogador);
                ctx.SaveChanges(); // <-- Salva no banco
            }
            return jogador;
        }

        public bool AjustarSaldo(int idJogador, decimal delta)
        {
            // Lógica ANTIGA:
            // var j = Db.Context.Jogadores.FirstOrDefault(x => x.IdJogador == idJogador);
            // ...

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                var j = ctx.Jogadores.FirstOrDefault(x => x.IdJogador == idJogador);
                if (j == null) return false;

                j.Saldo += delta;
                ctx.SaveChanges(); // <-- Salva a alteração do saldo
                return true;
            }
        }

        /// <summary>
        /// Obtém o jogador vinculado a um usuário específico (1:1 opcional).
        /// </summary>
        public Jogador ObterPorUsuarioId(int idUsuario)
        {
            // Lógica ANTIGA: Db.Context.Jogadores.FirstOrDefault(...)
            using (var ctx = new DataContext())
            {
                return ctx.Jogadores.FirstOrDefault(j => j.IdUsuario == idUsuario);
            }
        }
    }
}