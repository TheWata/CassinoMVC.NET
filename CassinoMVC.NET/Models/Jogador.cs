using System;

namespace CassinoMVC.Models
{
    public class Jogador
    {
        public int IdJogador { get; set; }
        public int? IdUsuario { get; set; }
        public string Apelido { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Navegação
        public Usuario Usuario { get; set; }
    }
}