using System;
using System.Collections.Generic;
using System.Linq;

namespace CassinoMVC.Models
{
    // Contêiner para lista de usuários, mantendo separação de responsabilidades (MVC)
    public class Usuarios
    {
        private readonly List<Usuario> _itens;

        public Usuarios(IEnumerable<Usuario> itens)
        {
            _itens = itens?.ToList() ?? new List<Usuario>();
        }

        public IReadOnlyList<Usuario> Itens => _itens;
        public int Count => _itens.Count;

        // Linhas já formatadas para UI (opcional)
        public IEnumerable<string> ToDisplayLines()
        {
            foreach (var u in _itens)
            {
                yield return string.Format("ID:{0} | {1} | {2} | Cargo:{3}", u.IdUsuario, u.NomeUsuario, u.NomeCompleto, u.Cargo);
            }
        }
    }
}
