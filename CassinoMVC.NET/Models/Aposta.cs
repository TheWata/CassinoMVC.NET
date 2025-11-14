using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CassinoMVC.Models
{
    [Table("Aposta")] // Você deve ter adicionado isso
    public class Aposta
    {
        [Key]
        public int IdAposta { get; set; }

        public int IdJogador { get; set; }

        public int? IdSessao { get; set; } // <-- MUDE AQUI (de int para int?)

        public decimal ValorApostado { get; set; }
        public string Resultado { get; set; } = string.Empty;
        public decimal ValorPremio { get; set; }
        public DateTime DataAposta { get; set; } = DateTime.UtcNow;

        [ForeignKey("IdJogador")]
        public Jogador Jogador { get; set; }

        [ForeignKey("IdSessao")]
        public SessaoJogo Sessao { get; set; }

        public string TipoDescricao { get; set; } = string.Empty;
        public bool EhSessao { get; set; }
        public int? DuracaoSegundos { get; set; }
    }
}