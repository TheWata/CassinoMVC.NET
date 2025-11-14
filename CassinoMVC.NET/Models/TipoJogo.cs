using System;
using System.ComponentModel.DataAnnotations;
namespace CassinoMVC.Models
{
    public class TipoJogo
    {
        [Key]
        public int IdTipoJogo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal VantagemCasa { get; set; }
    }
}