using System.Collections.Generic;
using CassinoMVC.Models;
using System.Linq;
using System;
using System.Data.Entity;

namespace CassinoMVC.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=CassinoDb")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<TipoJogo> TiposJogo { get; set; }
        public DbSet<SessaoJogo> Sessoes { get; set; }
        public DbSet<Aposta> Apostas { get; set; }

        // ADICIONADO: Mapeia a classe RegistroModel para uma tabela "RegistroModels"
        public DbSet<RegistroModel> Registros { get; set; }


        // O método SeedDefaults() pode ser removido ou adaptado
        // para um "Database Initializer" do EF, mas vamos deixá-lo
        // por enquanto, pois não será chamado automaticamente.
        public void SeedDefaults()
        {
            // ... (código do Seed) ...
        }
    }
}