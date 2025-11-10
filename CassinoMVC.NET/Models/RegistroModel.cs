using System;

namespace CassinoMVC.Models
{
    /// <summary>
    /// Registro de sessão de jogo / compra.
    /// Simplificado: um item por abertura de tela (ou compra) contendo saldo inicial/final.
    /// </summary>
    public class RegistroModel
    {
        public int Id { get; set; } // Id da sessão
        public string Usuario { get; set; } = string.Empty;
        public string JogoOuAcao { get; set; } = string.Empty; // Roleta | Blackjack | Caça-níqueis | Compra de fichas
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal Lucro => SaldoFinal - SaldoInicial;
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public int DuracaoSegundos => (int)(Fim - Inicio).TotalSeconds;
    }
}