﻿using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

/* Essa classe vai ser responsável por 
 * traduzir uma entidade em querys pra
 * inserir no banco de dados e também
 * o contrário, traduzir uma query que
 * vai recuperar valores, em uma entidade. */

namespace Journey.Infrastructure
{
    public class JourneyDbContext : DbContext
    {
        // DbSet<Trip> Trips -> Informa a entidade e é um acesso direto a tabela Trips no DbContext.
        public DbSet<Trip> Trips { get; set; }


        // optionsBuilder -> Configurando o tradutor, para traduzir uma entidade, para ter acesso ao banco de dados.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = I:\\Journey\\JourneyDatabase.db");
        }

        // Para acessar as atividades através da entidade Trip sem precisar de um acesso/link direto a tabela de Activities.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mostrando pro Entity que a entidade Activity vai ser pega da tabela Activities.
            modelBuilder.Entity<Activity>().ToTable("Activities");
        }
    }
}
