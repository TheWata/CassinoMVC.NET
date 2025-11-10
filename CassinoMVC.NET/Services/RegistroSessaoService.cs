using System.Collections.Generic;
using System.Linq;
using CassinoMVC.Models;

namespace CassinoMVC.Services
{
    /// <summary>
    /// Serviço simples em memória para gerenciar início/fim de sessões de jogo e compras.
    /// </summary>
    public static class RegistroSessaoService
    {
        private static readonly List<RegistroModel> _registros = new List<RegistroModel>();
        private static int _nextId = 1;

        /// <summary>
        /// Inicia sessão e retorna id.
        /// </summary>
        public static int IniciarSessao(string jogoOuAcao, string usuario, decimal saldoInicial)
        {
            var reg = new RegistroModel
            {
                Id = _nextId++,
                Usuario = usuario,
                JogoOuAcao = jogoOuAcao,
                SaldoInicial = saldoInicial,
                SaldoFinal = saldoInicial, // será atualizado
                Inicio = System.DateTime.UtcNow,
                Fim = System.DateTime.UtcNow
            };
            _registros.Add(reg);
            return reg.Id;
        }

        /// <summary>
        /// Finaliza sessão (atualiza saldo final e tempo).
        /// </summary>
        public static void FinalizarSessao(int id, decimal saldoFinal)
        {
            var reg = _registros.FirstOrDefault(r => r.Id == id);
            if (reg == null) return;
            reg.SaldoFinal = saldoFinal;
            reg.Fim = System.DateTime.UtcNow;
        }

        /// <summary>
        /// Lista registros com filtros opcionais de usuário ou jogo/ação.
        /// </summary>
        public static List<RegistroModel> Listar(string usuario, string jogoOuAcao)
        {
            var q = _registros.AsEnumerable();
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