using System;

namespace CassinoMVC.Utils
{
    public static class JogoUtils
    {
        // Random simples e compatível com C#7.3 / .NET Framework4.7.2
        private static readonly Random _rng = new Random();

        public static int NextInt(int minInclusive, int maxExclusive)
        {
            return _rng.Next(minInclusive, maxExclusive);
        }

        public static string FormatarDecimal(decimal valor)
        {
            return valor.ToString("F2");
        }
    }
}