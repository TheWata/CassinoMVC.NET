using System.Linq;
using CassinoMVC.Data;
using CassinoMVC.Models;
using System.Data.Entity; // <-- ADICIONADO (Necessário para .Include() se for usar)

namespace CassinoMVC.Services
{
    public static class RelatorioService
    {
        // ... (Classes DTO e ResumoApostas permanecem iguais) ...
        public class ResumoApostas
        {
            public int? IdJogador { get; set; }
            public string Apelido { get; set; }
            public decimal TotalApostado { get; set; }
            public decimal TotalGanho { get; set; }
            public decimal TotalPerdido { get; set; }
            public int QuantidadeApostas { get; set; }
        }
        public class ApostaConsultaDto
        {
            public int IdAposta { get; set; }
            public System.DateTime DataAposta { get; set; }
            public string NomeUsuario { get; set; }
            public string ApelidoJogador { get; set; }
            public int IdSessao { get; set; }
            public string NomeJogo { get; set; } = string.Empty;
            public decimal ValorApostado { get; set; }
            public string Resultado { get; set; } = string.Empty;
            public decimal ValorPremio { get; set; }
        }

        public static void LogSessaoAposta(string nomeTipoJogo, int idJogador, decimal valorApostado, string resultado, decimal valorPremio, string resumoSessao)
        {

            using (var ctx = new DataContext())
            {

                var tipo = ctx.TiposJogo.FirstOrDefault(t => t.Nome.Equals(nomeTipoJogo, System.StringComparison.OrdinalIgnoreCase));

                int idSessao = 0;
                if (tipo != null)
                {
                    var sessao = new SessaoJogo
                    {
                        // IdSessao = ctx.NextSessaoId(), // <-- REMOVIDO
                        IdTipoJogo = tipo.IdTipoJogo,
                        DataInicio = System.DateTime.UtcNow,
                        DataFim = System.DateTime.UtcNow,
                        Resultado = resumoSessao
                    };
                    ctx.Sessoes.Add(sessao);

                }

                var aposta = new Aposta
                {
                    // IdAposta = ctx.NextApostaId(), // <-- REMOVIDO
                    IdJogador = idJogador,
                    IdSessao = idSessao, // Isso pode ser um problema se idSessao for 0
                    ValorApostado = valorApostado,
                    Resultado = resultado,
                    ValorPremio = valorPremio,
                    DataAposta = System.DateTime.UtcNow,
                    TipoDescricao = tipo == null ? nomeTipoJogo : string.Empty,
                    EhSessao = false
                };


                ctx.Apostas.Add(aposta);
                ctx.SaveChanges();
            }
        }

        public static System.Collections.Generic.List<ApostaConsultaDto> ConsultarApostas(string nomeUsuario, string nomeTipoJogo)
        {
            // Lógica ANTIGA:
            // var ctx = Db.Context;

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                // A consulta LINQ original é compatível com EF
                var query = from a in ctx.Apostas
                            join s in ctx.Sessoes on a.IdSessao equals s.IdSessao into sa
                            from s in sa.DefaultIfEmpty()
                            join tj in ctx.TiposJogo on (s != null ? s.IdTipoJogo : 0) equals tj.IdTipoJogo into stj
                            from tj in stj.DefaultIfEmpty()
                            join j in ctx.Jogadores on a.IdJogador equals j.IdJogador
                            join u in ctx.Usuarios on j.IdUsuario equals u.IdUsuario into ju
                            from u in ju.DefaultIfEmpty()
                            select new ApostaConsultaDto
                            {
                                IdAposta = a.IdAposta,
                                DataAposta = a.DataAposta,
                                NomeUsuario = u != null ? u.NomeUsuario : null,
                                ApelidoJogador = j.Apelido,
                                IdSessao = (int)a.IdSessao,
                                NomeJogo = string.IsNullOrEmpty(a.TipoDescricao) ? (tj != null ? tj.Nome : "") : a.TipoDescricao,
                                ValorApostado = a.ValorApostado,
                                Resultado = a.Resultado,
                                ValorPremio = a.ValorPremio
                            };

                if (!string.IsNullOrWhiteSpace(nomeUsuario))
                {
                    var filtro = nomeUsuario.Trim().ToLowerInvariant();
                    query = query.Where(x => (x.NomeUsuario ?? string.Empty).ToLowerInvariant().Contains(filtro));
                }
                if (!string.IsNullOrWhiteSpace(nomeTipoJogo))
                {
                    var filtroJogo = nomeTipoJogo.Trim().ToLowerInvariant();
                    query = query.Where(x => x.NomeJogo.ToLowerInvariant() == filtroJogo);
                }

                return query.OrderByDescending(x => x.DataAposta).ThenByDescending(x => x.IdAposta).ToList();
            }
        }

        public static ResumoApostas GetResumoJogador(int idJogador)
        {

            using (var ctx = new DataContext())
            {
                var apostas = ctx.Apostas.Where(a => a.IdJogador == idJogador).ToList();
                var jogador = ctx.Jogadores.FirstOrDefault(j => j.IdJogador == idJogador);
                return new ResumoApostas
                {
                    IdJogador = idJogador,
                    Apelido = jogador != null ? jogador.Apelido : null,
                    TotalApostado = apostas.Sum(a => a.ValorApostado),
                    TotalGanho = apostas.Where(a => a.Resultado == "Ganhou" || a.Resultado.StartsWith("Sessão Ganhou")).Sum(a => a.ValorPremio),
                    TotalPerdido = apostas.Where(a => a.Resultado == "Perdeu" || a.Resultado.StartsWith("Sessão Perdeu")).Sum(a => a.ValorApostado),
                    QuantidadeApostas = apostas.Count
                };
            }
        }

        public static ResumoApostas GetResumoGeral()
        {
            // Lógica ANTIGA:
            // var ctx = Db.Context;

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                var apostas = ctx.Apostas.ToList();
                return new ResumoApostas
                {
                    IdJogador = null,
                    Apelido = null,
                    TotalApostado = apostas.Sum(a => a.ValorApostado),
                    TotalGanho = apostas.Where(a => a.Resultado == "Ganhou" || a.Resultado.StartsWith("Sessão Ganhou")).Sum(a => a.ValorPremio),
                    TotalPerdido = apostas.Where(a => a.Resultado == "Perdeu" || a.Resultado.StartsWith("Sessão Perdeu")).Sum(a => a.ValorApostado),
                    QuantidadeApostas = apostas.Count
                };
            }
        }
    }
}