using System;
using CassinoMVC.Models;

namespace CassinoMVC.Services
{
    // Service com funções de administração para preparar modelos de tela/edição
    public class AdminService
    {
        public class UsuarioFormModel
        {
            public bool ModoCriacao { get; set; }
            public string Titulo { get; set; }
            public string NomeCompleto { get; set; } = string.Empty;
            public string NomeUsuario { get; set; } = string.Empty;
            public CargoUsuario Cargo { get; set; } = CargoUsuario.Jogador;
            public string Senha { get; set; } = string.Empty; // vazio mantém senha
            public decimal Saldo { get; set; } // usado apenas em edição p/ Jogador
            public bool MostrarSaldo { get; set; }
        }

        // Prepara modelo para criação de usuário
        public UsuarioFormModel PrepararCriacaoUsuario()
        {
            return new UsuarioFormModel
            {
                ModoCriacao = true,
                Titulo = "Adicionar Usuário",
                Cargo = CargoUsuario.Jogador,
                Senha = string.Empty,
                Saldo = 0m,
                MostrarSaldo = false
            };
        }

        // Prepara modelo para edição de usuário existente
        public UsuarioFormModel PrepararEdicaoUsuario(Usuario usuario, decimal saldoJogador)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            return new UsuarioFormModel
            {
                ModoCriacao = false,
                Titulo = "Editar Usuário",
                NomeCompleto = usuario.NomeCompleto,
                NomeUsuario = usuario.NomeUsuario,
                Cargo = usuario.Cargo,
                Senha = string.Empty,
                Saldo = saldoJogador,
                MostrarSaldo = usuario.Cargo == CargoUsuario.Jogador
            };
        }
    }
}
