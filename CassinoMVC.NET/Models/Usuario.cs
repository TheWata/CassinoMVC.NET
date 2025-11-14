using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CassinoMVC.Models
{
    public enum CargoUsuario
    {
        Administrador,
        Jogador
    }

    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public CargoUsuario Cargo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Navegação 1:1 para Jogador (opcional)
        public virtual ICollection<Jogador> Jogadores { get; set; }
    }
}