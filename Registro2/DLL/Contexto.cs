using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Registro2.Entidades;

namespace Registro2.DLL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Inscripciones> Inscripciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = LUISDAVIDSO\SQLEXPRESS; Database = PersonasDb2; Trusted_Connection = True");
        }      
    }
}
