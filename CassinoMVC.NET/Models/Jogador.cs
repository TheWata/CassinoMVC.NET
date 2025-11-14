using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CassinoMVC.Models
{
    public class Jogador
    {
        [Key]
        public int IdJogador { get; set; }
        public int? IdUsuario { get; set; }
        public string Apelido { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Navegação
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; } // <-- ADICIONAR "virtual"
    }
}