using System.Collections.Generic;
using CassinoMVC.Enums;
using CassinoMVC.Models;
using CassinoMVC.Services;

namespace CassinoMVC.Controllers
{
    /// <summary>
    /// Controller para orquestrar apostas de roleta com dois grupos de regras:
    /// - Apostas internas (Inside): seleção de 1 a 3 números (pagam mais, chance menor).
    ///   • 1 número (Straight): 35:1
    ///   • 2 números (Split):   17:1
    ///   • 3 números (Street):  11:1
    /// - Apostas externas (Outside): vermelho/preto, par/ímpar e dezenas (pagam menos, chance maior).
    ///   • Vermelho/Preto: 1:1
    ///   • Par/Ímpar:      1:1
    ///   • Dezenas:        2:1
    /// </summary>
    public class RoletaController
    {
        public System.Tuple<int, CorRoleta, bool, decimal> ApostarInterna(System.Collections.Generic.IReadOnlyCollection<int> numeros)
        {
            return JogoRoletaService.ApostarInterna(numeros);
        }

        /// <summary>
        /// Tipos de apostas externas (apenas uma pode ser selecionada por vez na UI).
        /// </summary>
        public enum ApostaExternaTipo
        {
            Vermelho,
            Preto,
            Par,
            Impar,
            Dezenas_1_12,
            Dezenas_13_24,
            Dezenas_25_36
        }

        /// <summary>
        /// Processa aposta externa (uma única escolha). Calcula resultado e multiplicador.
        /// Multiplicadores: 1:1 para Vermelho/Preto/Par/Ímpar; 2:1 para Dezenas.
        /// </summary>
        public System.Tuple<int, CorRoleta, bool, decimal> ApostarExterna(ApostaExternaTipo tipo)
        {
            return JogoRoletaService.ApostarExterna((JogoRoletaService.ApostaExternaTipo)tipo);
        }
    }
}