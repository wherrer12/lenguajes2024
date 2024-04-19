using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Tarea_3_Clinica_Dental.Models
{
    public class DbContextClinicaDental : DbContext
    {
        public DbContextClinicaDental(DbContextOptions<DbContextClinicaDental> options) : base(options)
        {


        }

        public DbSet<Paciente> Pacientes { get; set; }

        //Para que cargue una fila con datos para probar la tabla
       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>().HasData(new Paciente()
            {
                Cedula = "234556781",
                Nombre = "Antonio Mora Henz",
                Email = "anto1@gmail.net",
                Fecha = DateTime.Now,
                Procedimiento = "Limpieza",
                
            });
        }*/

    }//Fin clase

}//Fin namespace
