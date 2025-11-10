namespace CassinoMVC.Models
{
    public class Carta
    {
        public int IdCarta { get; set; }
        public string Naipe { get; set; } = string.Empty; // Copas, Espadas, Ouros, Paus
        public string Valor { get; set; } = string.Empty; // A, 2..10, J, Q, K
        public int Pontuacao { get; set; }
        public string ImagemPath { get; set; } // relativo ao diretório de saída
    }
}