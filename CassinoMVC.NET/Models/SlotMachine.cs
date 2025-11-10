using System;
using System.Collections.Generic;
using System.Linq;

namespace CassinoMVC.Models
{
    /// <summary>
    /// Modelo da slot machine (caça-níqueis). Mantém os símbolos, resultado do último giro e multiplicador.
    /// Regras:
    /// - Sempre gira 5 rolos.
    /// - 3 símbolos iguais: dobra (x2) o valor apostado.
    /// - 4 símbolos iguais: triplica (x3).
    /// - 5 símbolos iguais: quadruplica (x4).
    /// - Caso contrário não há prêmio (x0).
    /// </summary>
    public class SlotMachine
    {
        public int IdSlot { get; set; }
        public List<string> Simbolos { get; set; } = new List<string>{"1","2","3","4","5","6","7"}; //7 símbolos
        public string Resultado { get; set; } = string.Empty;
        public decimal MultiplicadorGanho { get; set; }
        public DateTime DataJogo { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Realiza o giro sorteando um símbolo por rolo.
        /// </summary>
        public string[] Girar(int rolos =5, Random rng = null)
        {
            if (rng == null) rng = new Random();
            var resultado = Enumerable.Range(0, rolos)
                .Select(_ => Simbolos[rng.Next(Simbolos.Count)])
                .ToArray();
            Resultado = string.Join("|", resultado);
            MultiplicadorGanho = CalcularMultiplicador(resultado);
            return resultado;
        }

        /// <summary>
        /// Calcula multiplicador com base na maior frequência de um símbolo.
        /// </summary>
        private static decimal CalcularMultiplicador(IReadOnlyList<string> r)
        {
            if (r.Count ==0) return 0m;
            var grupos = r.GroupBy(s => s).Select(g => g.Count()).OrderByDescending(c => c).ToList();
            var max = grupos.First();
            switch (max)
            {
                case 5: return 4m;
                case 4: return 3m;
                case 3: return 2m;
                default: return 0m;
            }
        }
    }
}