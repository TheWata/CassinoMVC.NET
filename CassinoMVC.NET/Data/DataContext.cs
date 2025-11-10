using System.Collections.Generic;
using CassinoMVC.Models;
using System.Linq;
using System;

namespace CassinoMVC.Data
{
    public class DataContext
    {
        // Coleções em memória (simples, substituir por DB depois)
        public List<Usuario> Usuarios { get; } = new List<Usuario>();
        public List<Jogador> Jogadores { get; } = new List<Jogador>();
        public List<TipoJogo> TiposJogo { get; } = new List<TipoJogo>();
        public List<SessaoJogo> Sessoes { get; } = new List<SessaoJogo>();
        public List<Aposta> Apostas { get; } = new List<Aposta>();

        private int _idUsuario = 1;
        private int _idJogador = 1;
        private int _idTipoJogo = 1;
        private int _idSessao = 1;
        private int _idAposta = 1;

        public int NextUsuarioId() { return _idUsuario++; }
        public int NextJogadorId() { return _idJogador++; }
        public int NextTipoJogoId() { return _idTipoJogo++; }
        public int NextSessaoId() { return _idSessao++; }
        public int NextApostaId() { return _idAposta++; }

        public void SeedDefaults()
        {
            if (TiposJogo.Count == 0)
            {
                TiposJogo.AddRange(new TipoJogo[]{
                    new TipoJogo{ IdTipoJogo = NextTipoJogoId(), Nome = "Roleta", Descricao = "Aposte em números, cores ou par/ímpar.", VantagemCasa = 2.7m},
                    new TipoJogo{ IdTipoJogo = NextTipoJogoId(), Nome = "Blackjack", Descricao = "Chegue o mais perto de 21 sem passar.", VantagemCasa = 1.5m},
                    new TipoJogo{ IdTipoJogo = NextTipoJogoId(), Nome = "Caça-níqueis", Descricao = "Gire e combine símbolos.", VantagemCasa = 5m}
                });
            }
            if (!Usuarios.Any())
            {
                var admin = new Usuario();
                admin.IdUsuario = NextUsuarioId();
                admin.NomeCompleto = "Administrador do Sistema";
                admin.NomeUsuario = "admin";
                admin.SenhaHash = Services.UsuarioService.GerarHashSenha("admin");
                admin.Cargo = CargoUsuario.Administrador;
                admin.DataCriacao = DateTime.UtcNow;
                Usuarios.Add(admin);

                // Cria um jogador padrão para facilitar testes nas telas
                var jogador = new Jogador();
                jogador.IdJogador = NextJogadorId();
                jogador.IdUsuario = admin.IdUsuario;
                jogador.Apelido = "Player1";
                jogador.Saldo = 1000m;
                jogador.Email = "player1@example.com";
                jogador.DataCriacao = DateTime.UtcNow;
                Jogadores.Add(jogador);
            }
        }
    }

    public static class Db
    {
        // Instância única em memória
        public static readonly DataContext Context = new DataContext();
        static Db()
        {
            Context.SeedDefaults();
        }
    }
}