using Microsoft.EntityFrameworkCore;

namespace Carona_Service.Models
{
    public class Carona_ServiceContext : DbContext
    {
        public Carona_ServiceContext (DbContextOptions<Carona_ServiceContext> options)
            : base(options)
        {
        }

        public DbSet<CaronaOferta> CaronaOferta { get; set; }

        public DbSet<CaronaBusca> CaronaBusca { get; set; }
        
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Veiculo> Veiculo { get; set; }

    }
}
