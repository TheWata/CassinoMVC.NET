using System.Collections.Generic;

namespace CassinoMVC.Models
{
    public enum BlackjackEstado
    {
        EmCurso,
        Finalizada
    }

    public class BlackjackGame
    {
        public Baralho Baralho { get; set; } = new Baralho();
        public List<Carta> MaoJogador { get; set; } = new List<Carta>();
        public List<Carta> MaoDealer { get; set; } = new List<Carta>();
        public decimal ApostaInicial { get; set; }
        public decimal ApostaAtual { get; set; }
        public bool DoubleDownUsado { get; set; }
        public BlackjackEstado Estado { get; set; } = BlackjackEstado.EmCurso;
    }
}