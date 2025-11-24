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
    }
}
