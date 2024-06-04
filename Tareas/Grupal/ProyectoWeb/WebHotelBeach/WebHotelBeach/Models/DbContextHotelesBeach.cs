using Microsoft.EntityFrameworkCore;

namespace WebHotelBeach.Models
{
    public class DbContextHotelesBeach : DbContext
    {
        public DbContextHotelesBeach (DbContextOptions<DbContextHotelesBeach> options) : base (options)
        {

        }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Paquete> Paquete { get; set;}

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente"); 
        }

    }//Cierre class
}//Cierre namespace
