using Microsoft.EntityFrameworkCore;

namespace Examen_1_B43383.Models
{
    public class DbContextCita : DbContext
    {

        public DbContextCita(DbContextOptions<DbContextCita> options) : base(options)
        {

        }

        public DbSet<Cita> citas { get; set; }

    }
}


