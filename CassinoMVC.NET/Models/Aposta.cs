using System;

namespace CassinoMVC.Models
{
    public class Aposta
    {
        public int IdAposta { get; set; }
        public int IdJogador { get; set; }
        public int IdSessao { get; set; }
        public decimal ValorApostado { get; set; }
        public string Resultado { get; set; } = string.Empty; // Ganhou, Perdeu, Empate, Sessão
        public decimal ValorPremio { get; set; }
        public DateTime DataAposta { get; set; } = DateTime.UtcNow;

        // Navegação
        public Jogador Jogador { get; set; }
        public SessaoJogo Sessao { get; set; }

        // Tipo simples para relatório especial (ex.: Compra de fichas)
        public string TipoDescricao { get; set; } = string.Empty;

        // Flag para indicar registro agregado de sessão de jogo
        public bool EhSessao { get; set; }
        // Duração da sessão em segundos (somente se EhSessao = true)
        public int? DuracaoSegundos { get; set; }
    }
}