using System.Collections.Generic;
using System.Linq;
using CassinoMVC.Models;
using CassinoMVC.Data;

namespace CassinoMVC.Services
{
    public static class RegistroSessaoService
    {
        public static int IniciarSessao(string jogoOuAcao, string usuario, decimal saldoInicial)
        {
            var reg = new RegistroModel
            {
                Usuario = usuario,
                JogoOuAcao = jogoOuAcao,
                SaldoInicial = saldoInicial,
                SaldoFinal = saldoInicial,
                Inicio = System.DateTime.UtcNow,
                Fim = System.DateTime.UtcNow
            };

            using (var ctx = new DataContext())
            {
                ctx.Registros.Add(reg);
                ctx.SaveChanges();
            }

            return reg.Id;
        }


        /// <summary>
        /// Finaliza sessão (atualiza saldo final e tempo no banco).
        /// </summary>
        public static void FinalizarSessao(int idSessaoRegistro, int idJogador, decimal saldoFinal)
        {
            using (var ctx = new DataContext())
            {
                // 1. Atualiza a tabela Registros
                var reg = ctx.Registros.Find(idSessaoRegistro);
                if (reg != null)
                {
                    reg.SaldoFinal = saldoFinal;
                    reg.Fim = System.DateTime.UtcNow;
                }

                // 2. ADICIONADO: Atualiza a tabela Jogadores
                var jogador = ctx.Jogadores.Find(idJogador);
                if (jogador != null)
                {
                    jogador.Saldo = saldoFinal;
                }

                ctx.SaveChanges(); // Salva AMBAS as alterações
            }
        }

        public static List<RegistroModel> Listar(string usuario, string jogoOuAcao)
        {
            using (var ctx = new DataContext())
            {
                var q = ctx.Registros.AsQueryable();

                if (!string.IsNullOrWhiteSpace(usuario))
                {
                    var f = usuario.ToLower();
                    q = q.Where(r => r.Usuario != null && r.Usuario.ToLower().Contains(f));
                }

                if (!string.IsNullOrWhiteSpace(jogoOuAcao))
                {
                    var f = jogoOuAcao.ToLower();
                    q = q.Where(r => r.JogoOuAcao != null && r.JogoOuAcao.ToLower() == f);
                }

                return q.OrderByDescending(r => r.Inicio).ToList();
            }
        }
    }
}