using Microsoft.EntityFrameworkCore;

namespace AppBibliotecaWebG1.Models
{

    //Muy importante que herede de la clase padre DbContext
    public class DbContextBiblioteca : DbContext //Herencia
    {

        ///<summary>   
        ///Constructor con parametros recibe la informacion del ORM
        ///</summary>
        ///<param name="options"></param>"
        public DbContextBiblioteca(DbContextOptions<DbContextBiblioteca> options) : base(options)
        {

        }

        //Propiedad DbSet que permite dar mantenimiento al catalogo de los libros
        public DbSet<Libro> Libros { get; set; }

        //Propiedad DbSet que permite dar mantenimiento al catalogo de los usuarios
        public DbSet<Usuario> Usuarios { get; set; }

        //Metodo encargado de crear la tabla para la entidad en la bd
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>().HasData(new Libro()
            {
                ISBN = 1,
                Titulo = "Lenguajes programacion",
                Editorial = "Puntarenas",
                PrecioVenta = 27500,
                Foto = "ND",
                FechaPublicacion = DateTime.Now,
                Estado = 'A'
            });
        }

    }//Fin class
}// Fin namespace
