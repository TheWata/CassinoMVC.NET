using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CassinoMVC.Models
{
    public class SessaoJogo
    {
        [Key]
        public int IdSessao { get; set; }
        public int IdTipoJogo { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.UtcNow;
        public DateTime? DataFim { get; set; }
        public string Resultado { get; set; } = string.Empty;

        // Navegação
        [ForeignKey("IdTipoJogo")] // <-- ADICIONAR
        public TipoJogo TipoJogo { get; set; }
    }
}