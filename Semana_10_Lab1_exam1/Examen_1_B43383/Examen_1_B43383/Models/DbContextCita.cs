using Microsoft.EntityFrameworkCore;

namespace Examen_1_B43383.Models
{
    public class DbContextCita : DbContext
    {
        public DbContextCita()
        {
        }

        public DbContextCita(DbContextOptions<DbContextCita> options) : base(options)
        {

        }

        public DbSet<Cita> Citas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        //Con objeto diagnostico como entidad
        //public DbSet<Diagnostico> Diagnosticos { get; set; }

    }
}


