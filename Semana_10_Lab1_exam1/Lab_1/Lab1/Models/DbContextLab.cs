using Microsoft.EntityFrameworkCore;

namespace Lab1.Models
{
    public class DbContextLab : DbContext
    {
        public DbContextLab(DbContextOptions<DbContextLab> options) : base(options)
        {

        }
        public DbSet<Paciente> Pacientes { get; set; }
    }
   
}
