using System;
using CassinoMVC.Enums;

namespace CassinoMVC.Models
{
    public class Roleta
    {
        public int IdRoleta { get; set; }
        public int NumeroSorteado { get; set; }
        public CorRoleta Cor { get; set; }
        public TipoApostaRoleta TipoAposta { get; set; }
        public decimal Multiplicador { get; set; }
        public DateTime DataJogo { get; set; } = DateTime.UtcNow;

        private static readonly int[] Vermelhos = new int[] {1,3,5,7,9,12,14,16,18,19,21,23,25,27,30,32,34,36 };

        public static CorRoleta ObterCor(int numero)
        {
            if (numero ==0) return CorRoleta.Verde;
            return Array.IndexOf(Vermelhos, numero) >=0 ? CorRoleta.Vermelho : CorRoleta.Preto;
        }

        public static int SortearNumero(Random rng = null)
        {
            if (rng == null) rng = new Random();
            return rng.Next(0,37);
        }

        public static decimal CalcularMultiplicador(TipoApostaRoleta tipo)
        {
            switch (tipo)
            {
                case TipoApostaRoleta.Numero:
                    return 36m;
                case TipoApostaRoleta.Cor:
                    return 2m;
                case TipoApostaRoleta.ParImpar:
                    return 2m;
                default:
                    return 1m;
            }
        }
    }
}