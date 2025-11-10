using System;
using System.Collections.Generic;
using System.Linq;

namespace CassinoMVC.Models
{
    public class Baralho
    {
        public int IdBaralho { get; set; }
        public List<Carta> Cartas { get; set; } = new List<Carta>();

        private static readonly string[] Naipes = { "Copas", "Espadas", "Ouros", "Paus" };
        private static readonly string[] Valores = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        public static Baralho NovoBaralho(string imagensBasePath = "Resources/PNG-cards-1.3")
        {
            var baralho = new Baralho();
            foreach (var naipe in Naipes)
            {
                foreach (var valor in Valores)
                {
                    baralho.Cartas.Add(new Carta
                    {
                        Naipe = naipe,
                        Valor = valor,
                        Pontuacao = ValorParaPontuacao(valor),
                        ImagemPath = GerarImagemPath(imagensBasePath, valor, naipe)
                    });
                }
            }
            return baralho;
        }

        private static string GerarImagemPath(string basePath, string valor, string naipe)
        {
            string naipeFile;
            switch (naipe)
            {
                case "Copas": naipeFile = "hearts"; break;
                case "Espadas": naipeFile = "spades"; break;
                case "Ouros": naipeFile = "diamonds"; break;
                case "Paus": naipeFile = "clubs"; break;
                default: naipeFile = string.Empty; break;
            }

            string valorFile;
            switch (valor)
            {
                case "A": valorFile = "ace"; break;
                case "J": valorFile = "jack"; break;
                case "Q": valorFile = "queen"; break;
                case "K": valorFile = "king"; break;
                default: valorFile = valor; break;
            }

            // combina os nomes para formar o nome do arquivo
            return System.IO.Path.Combine(basePath, string.Format("{0}_of_{1}.png", valorFile, naipeFile));
        }

        public void Embaralhar(Random rng = null)
        {
            if (rng == null) rng = new Random();
            Cartas = Cartas.OrderBy(_ => rng.Next()).ToList();
        }

        public Carta ComprarCarta()
        {
            if (Cartas.Count == 0) return null;
            var carta = Cartas[0];
            Cartas.RemoveAt(0);
            return carta;
        }

        private static int ValorParaPontuacao(string valor)
        {
            switch (valor)
            {
                case "A": return 11;
                case "J": return 10;
                case "Q": return 10;
                case "K": return 10;
                default:
                    int n;
                    return int.TryParse(valor, out n) ? n : 0;
            }
        }
    }
}