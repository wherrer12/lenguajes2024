//Libreria a usar
using Microsoft.EntityFrameworkCore;

namespace ApiHotelBeach.Model
{
    //Clase para la conexion a la base de datos
    public class DbContextHotelesBeach : DbContext
    {
        //Constructor de la clase
        public DbContextHotelesBeach (DbContextOptions<DbContextHotelesBeach> options) : base (options)
        {

        }

        //Objeto cliente para la conexion con base de datos
        public DbSet<Cliente> Cliente { get; set; }

        //Objeto cliente para la conexion con base de datos
        public DbSet<Paquete> Paquete { get; set;}

        //Referencia a la tabla cliente
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //se asigna el nombre de la tabla
            modelBuilder.Entity<Cliente>().ToTable("Cliente"); 
        }

    }//Cierre class
}//Cierre namespace
