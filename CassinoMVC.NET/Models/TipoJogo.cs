using System;

namespace CassinoMVC.Models
{
    public class TipoJogo
    {
        public int IdTipoJogo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal VantagemCasa { get; set; }
    }
}