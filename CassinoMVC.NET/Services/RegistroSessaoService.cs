using System.Collections.Generic;
using System.Linq;
using CassinoMVC.Models;
using CassinoMVC.Data; // <-- ADICIONADO

namespace CassinoMVC.Services
{
    /// <summary>
    /// Serviço para gerenciar início/fim de sessões de jogo e compras.
    /// AGORA USANDO O BANCO DE DADOS (EF).
    /// </summary>
    public static class RegistroSessaoService
    {
        // REMOVIDO o banco de dados em memória
        // private static readonly List<RegistroModel> _registros = new List<RegistroModel>();
        // private static int _nextId = 1;

        /// <summary>
        /// Inicia sessão e retorna id. (Salva no banco)
        /// </summary>
        public static int IniciarSessao(string jogoOuAcao, string usuario, decimal saldoInicial)
        {
            var reg = new RegistroModel
            {
                // Id = _nextId++, // <-- REMOVIDO (será definido pelo SQL Server)
                Usuario = usuario,
                JogoOuAcao = jogoOuAcao,
                SaldoInicial = saldoInicial,
                SaldoFinal = saldoInicial, // será atualizado
                Inicio = System.DateTime.UtcNow,
                Fim = System.DateTime.UtcNow
            };

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                ctx.Registros.Add(reg);
                ctx.SaveChanges(); // Salva no banco
            }

            return reg.Id; // Retorna o ID gerado pelo banco
        }

        /// <summary>
        /// Finaliza sessão (atualiza saldo final e tempo no banco).
        /// </summary>
        public static void FinalizarSessao(int id, decimal saldoFinal)
        {
            // Lógica ANTIGA:
            // var reg = _registros.FirstOrDefault(r => r.Id == id);

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                var reg = ctx.Registros.Find(id); // Busca pela Chave Primária
                if (reg == null) return;

                reg.SaldoFinal = saldoFinal;
                reg.Fim = System.DateTime.UtcNow;

                ctx.SaveChanges(); // Salva as alterações
            }
        }

        /// <summary>
        /// Lista registros com filtros opcionais de usuário ou jogo/ação (do banco).
        /// </summary>
        public static List<RegistroModel> Listar(string usuario, string jogoOuAcao)
        {
            // Lógica ANTIGA:
            // var q = _registros.AsEnumerable();

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                var q = ctx.Registros.AsQueryable(); // Começa a consulta no SQL

                if (!string.IsNullOrWhiteSpace(usuario))
                {
                    var f = usuario.ToLowerInvariant();
                    q = q.Where(r => r.Usuario.ToLowerInvariant().Contains(f));
                }
                if (!string.IsNullOrWhiteSpace(jogoOuAcao))
                {
                    var f = jogoOuAcao.ToLowerInvariant();
                    q = q.Where(r => r.JogoOuAcao.ToLowerInvariant() == f);
                }
                return q.OrderByDescending(r => r.Inicio).ToList();
            }
        }
    }
}