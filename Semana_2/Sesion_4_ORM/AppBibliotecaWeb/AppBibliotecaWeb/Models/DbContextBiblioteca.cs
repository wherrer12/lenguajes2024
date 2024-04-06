using AppBilbliotecaWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AppBibliotecaWeb.Models
{
    //La clase hereda de DbContext
    public class DbContextBiblioteca : DbContext
    {

        ///<summary>
        ///Constructor con parametros
        /// 
        /// </summary>
        /// 

        public DbContextBiblioteca(DbContextOptions<DbContextBiblioteca> options) : base(options) { 
        
        }

        //Se construye el DbSet para el catalogo de libros
        public DbSet<Libro> Libros { get; set; }

        //Metodo encargado de inicalizar el objeto modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Se crea la entidad con sus datos iniciales
            modelBuilder.Entity<Libro>().HasData(new Libro()
            { 
                ISBN=44,
                Titulo = "Lenguaje de programacion",
                Editorial = "Puntarenas",
                PrecioVenta = 25300,
                Foto = "ND",
                FechaPublicacion = DateTime.Now,
                Estado = 'A'
            }
                   
           );
        }

    }//Cierre class
}//Cierre namespace
