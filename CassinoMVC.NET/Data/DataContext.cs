using System.Collections.Generic;
using CassinoMVC.Models;
using System.Linq;
using System;
using System.Data.Entity; // <-- ADICIONADO

namespace CassinoMVC.Data
{
    // 1. Herda de DbContext
    public class DataContext : DbContext
    {
        // 2. Construtor que usa a string de conexão do App.config
        public DataContext() : base("name=CassinoDb")
        {
        }

        // 3. List<T> foram alteradas para DbSet<T>
        //    O Entity Framework vai ligar estas propriedades às suas tabelas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<TipoJogo> TiposJogo { get; set; }
        public DbSet<SessaoJogo> Sessoes { get; set; }
        public DbSet<Aposta> Apostas { get; set; }

        // 4. Todos os contadores de ID e métodos Next...Id() foram REMOVIDOS.
        //    O SQL Server (com colunas Identity) cuidará disso.

        // 5. O método SeedDefaults() permanece, mas NÃO será chamado
        //    automaticamente por enquanto. A forma de popular dados (seeding)
        //    muda com o EF, mas vamos manter o método por ora.
        public void SeedDefaults()
        {
            if (TiposJogo.Count() == 0) // .Count() do Linq, não .Count da Lista
            {
                TiposJogo.AddRange(new TipoJogo[]{
                    new TipoJogo{ Nome = "Roleta", Descricao = "Aposte em números, cores ou par/ímpar.", VantagemCasa = 2.7m},
                    new TipoJogo{ Nome = "Blackjack", Descricao = "Chegue o mais perto de 21 sem passar.", VantagemCasa = 1.5m},
                    new TipoJogo{ Nome = "Caça-níqueis", Descricao = "Gire e combine símbolos.", VantagemCasa = 5m}
                });
            }
            if (!Usuarios.Any())
            {
                var admin = new Usuario();
                admin.NomeCompleto = "Administrador do Sistema";
                admin.NomeUsuario = "admin";
                // Você DEVE ter o Services.UsuarioService aqui,
                // mas idealmente o Hashing não deveria estar no DataContext.
                // Por ora, vamos assumir que o serviço de Hashing está acessível.
                admin.SenhaHash = Services.UsuarioService.GerarHashSenha("admin");
                admin.Cargo = CargoUsuario.Administrador;
                admin.DataCriacao = DateTime.UtcNow;
                Usuarios.Add(admin);

                // Cria um jogador padrão
                var jogador = new Jogador();
                jogador.IdUsuario = admin.IdUsuario; // Isso pode falhar se o admin não foi salvo (sem ID)
                jogador.Apelido = "Player1";
                jogador.Saldo = 1000m;
                jogador.Email = "player1@example.com";
                jogador.DataCriacao = DateTime.UtcNow;
                Jogadores.Add(jogador);
            }

            // É preciso salvar as mudanças do Seed
            try
            {
                SaveChanges();
            }
            catch (Exception ex)
            {
                // Lidar com exceção de seeding
                Console.WriteLine(ex.Message);
            }
        }
    }

    // 6. A classe estática 'Db' FOI REMOVIDA.
    //
    //    O padrão 'static readonly Context' era ótimo para o modo "em memória",
    //    mas é um PÉSSIMO padrão para o Entity Framework.
    //
    //    O 'DbContext' (nosso DataContext) deve ser criado e descartado
    //    para cada operação ou conjunto de operações (padrão "Unit of Work").
    //
    //    Vamos corrigir os outros serviços para instanciar o DataContext
    //    usando 'using (var ctx = new DataContext()) { ... }'
    //
    /*
    public static class Db
    {
        // REMOVIDO!
    }
    */
}