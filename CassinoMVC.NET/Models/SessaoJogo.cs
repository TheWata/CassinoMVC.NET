using System;

namespace CassinoMVC.Models
{
    public class SessaoJogo
    {
        public int IdSessao { get; set; }
        public int IdTipoJogo { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.UtcNow;
        public DateTime? DataFim { get; set; }
        public string Resultado { get; set; } = string.Empty;

        // Navegação
        public TipoJogo TipoJogo { get; set; }
    }
}