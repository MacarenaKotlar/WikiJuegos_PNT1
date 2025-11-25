using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WikiJuegos_PNT1.Models;

namespace WikiJuegos_PNT1.Context
{
    public class WikiJuegosDatabaseContext : DbContext
    {
        public
        WikiJuegosDatabaseContext(DbContextOptions<WikiJuegosDatabaseContext> options)
        : base(options)
        {
        }
        public DbSet<Juego> Juegos { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(tabla =>
            {
                tabla.HasKey(col => col.Id);
                tabla.Property(col => col.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tabla.Property(col => col.Nombre).HasMaxLength(50);
                tabla.Property(col => col.Contra).HasMaxLength(50);
            });
        }
    }
}
