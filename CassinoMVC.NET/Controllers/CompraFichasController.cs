using System;
using CassinoMVC.Data;
using CassinoMVC.Models;
using System.Linq; // Adicionado para FirstOrDefault (embora trocado por Find)

namespace CassinoMVC.Controllers
{
    public class CompraFichasController
    {
        public bool Comprar(int idJogador, decimal valor)
        {
            if (valor <= 0) return false;

            using (var ctx = new DataContext())
            {
                var jogador = ctx.Jogadores.Find(idJogador);
                if (jogador == null) return false;

                jogador.Saldo += valor;

                var compra = new Aposta();
                compra.IdJogador = jogador.IdJogador;

                compra.IdSessao = null; // <-- MUDE AQUI (de 0 para null)

                compra.ValorApostado = valor;
                compra.Resultado = "Fichas Compradas";
                compra.ValorPremio = 0m;
                compra.DataAposta = System.DateTime.UtcNow;
                compra.TipoDescricao = "Compra de fichas";

                ctx.Apostas.Add(compra);

                ctx.SaveChanges(); // Agora deve funcionar
                return true;
            }
        }
    }
}