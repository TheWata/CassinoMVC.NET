using System;
using System.Collections.Generic;
using CassinoMVC.Models;
using CassinoMVC.Enums;

namespace CassinoMVC.Services
{
    /// <summary>
    /// Serviço que encapsula a lógica da roleta seguindo as regras usadas na UI/Controller.
    /// Oferece dois fluxos de aposta:
    /// - Interna (1 a 3 números): 1 número paga 35:1, 2 números 17:1, 3 números 11:1.
    /// - Externa (apenas um tipo): Vermelho/Preto/Par/Ímpar (1:1) e Dezenas 1-12/13-24/25-36 (2:1).
    /// Observação: multiplicador retornado é o retorno total (aposta + lucro). Ex.: 1:1 => 2, 35:1 => 36.
    /// </summary>
    public static class JogoRoletaService
    {
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

        public static Tuple<int, CorRoleta, bool, decimal> ApostarInterna(IReadOnlyCollection<int> numeros)
        {
            if (numeros.Count == 0 || numeros.Count > 3)
                throw new ArgumentException("Selecione entre 1 e 3 números.");

            int sorteado = Roleta.SortearNumero();
            var cor = Roleta.ObterCor(sorteado);
            bool ganhou = System.Linq.Enumerable.Contains(numeros, sorteado);
            decimal multiplicador;
            switch (numeros.Count)
            {
                case 1:
                    multiplicador = 36m;
                    break;
                case 2:
                    multiplicador = 18m;
                    break;
                case 3:
                    multiplicador = 12m;
                    break;
                default:
                    multiplicador = 0m;
                    break;
            }
            if (!ganhou) multiplicador = 0m;
            return new Tuple<int, CorRoleta, bool, decimal>(sorteado, cor, ganhou, multiplicador);
        }

        public static Tuple<int, CorRoleta, bool, decimal> ApostarExterna(ApostaExternaTipo tipo)
        {
            int sorteado = Roleta.SortearNumero();
            var cor = Roleta.ObterCor(sorteado);
            bool ganhou = false;
            decimal multiplicador;
            switch (tipo)
            {
                case ApostaExternaTipo.Vermelho:
                case ApostaExternaTipo.Preto:
                case ApostaExternaTipo.Par:
                case ApostaExternaTipo.Impar:
                    multiplicador = 2m;
                    break;
                case ApostaExternaTipo.Dezenas_1_12:
                case ApostaExternaTipo.Dezenas_13_24:
                case ApostaExternaTipo.Dezenas_25_36:
                    multiplicador = 3m;
                    break;
                default:
                    multiplicador = 0m;
                    break;
            }

            switch (tipo)
            {
                case ApostaExternaTipo.Vermelho:
                    ganhou = cor == CorRoleta.Vermelho;
                    break;
                case ApostaExternaTipo.Preto:
                    ganhou = cor == CorRoleta.Preto;
                    break;
                case ApostaExternaTipo.Par:
                    ganhou = sorteado != 0 && sorteado % 2 == 0;
                    break;
                case ApostaExternaTipo.Impar:
                    ganhou = sorteado % 2 == 1;
                    break;
                case ApostaExternaTipo.Dezenas_1_12:
                    ganhou = sorteado >= 1 && sorteado <= 12;
                    break;
                case ApostaExternaTipo.Dezenas_13_24:
                    ganhou = sorteado >= 13 && sorteado <= 24;
                    break;
                case ApostaExternaTipo.Dezenas_25_36:
                    ganhou = sorteado >= 25 && sorteado <= 36;
                    break;
            }

            if (!ganhou) multiplicador = 0m;
            return new Tuple<int, CorRoleta, bool, decimal>(sorteado, cor, ganhou, multiplicador);
        }

        #region Métodos Legados (compatibilidade com ApostaService)
        /// <summary>
        /// Mantido para compatibilidade: executa uma rodada simples baseada em TipoApostaRoleta legado.
        /// </summary>
        public static Roleta ExecutarRodada(TipoApostaRoleta tipoAposta, int? numeroEscolhido = null, CorRoleta? corEscolhida = null, bool? parEscolhido = null)
        {
            var numero = Roleta.SortearNumero();
            var cor = Roleta.ObterCor(numero);
            return new Roleta
            {
                IdRoleta = 0,
                NumeroSorteado = numero,
                Cor = cor,
                TipoAposta = tipoAposta,
                Multiplicador = Roleta.CalcularMultiplicador(tipoAposta)
            };
        }

        /// <summary>
        /// Avalia a aposta usando o modelo legado (Número, Cor, Par/Ímpar).
        /// </summary>
        public static (bool ganhou, decimal multiplicador) AvaliarAposta(Roleta rodada, TipoApostaRoleta tipoAposta, int? numeroEscolhido = null, CorRoleta? corEscolhida = null, bool? parEscolhido = null)
        {
            bool ganhou = false;
            switch (tipoAposta)
            {
                case TipoApostaRoleta.Numero:
                    ganhou = numeroEscolhido.HasValue && numeroEscolhido.Value == rodada.NumeroSorteado;
                    break;
                case TipoApostaRoleta.Cor:
                    ganhou = corEscolhida.HasValue && corEscolhida.Value == rodada.Cor;
                    break;
                case TipoApostaRoleta.ParImpar:
                    var ehPar = rodada.NumeroSorteado != 0 && rodada.NumeroSorteado % 2 == 0;
                    ganhou = parEscolhido.HasValue && parEscolhido.Value == ehPar;
                    break;
            }
            var mult = ganhou ? Roleta.CalcularMultiplicador(tipoAposta) : 0m;
            return (ganhou, mult);
        }
        #endregion
    }
}