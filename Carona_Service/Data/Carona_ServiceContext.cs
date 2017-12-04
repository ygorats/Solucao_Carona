using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Carona_Service.Models;
using System.Threading;

namespace Carona_Service.Models
{
    public class Carona_ServiceContext : DbContext
    {
        public Carona_ServiceContext (DbContextOptions<Carona_ServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Carona_Service.Models.CaronaOferta> CaronaOferta { get; set; }

        public DbSet<Carona_Service.Models.CaronaBusca> CaronaBusca { get; set; }
        
        public DbSet<Carona_Service.Models.Usuario> Usuario { get; set; }

        public DbSet<Carona_Service.Models.Veiculo> Veiculo { get; set; }

    }
}
