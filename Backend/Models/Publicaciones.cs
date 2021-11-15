using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class PublicacionesContext : DbContext
    {
        public PublicacionesContext(DbContextOptions<PublicacionesContext> options):base(options)
        {

        }
        public DbSet<Publicaciones> Publicacion { get; set;}
    }
    public class Publicaciones
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }
}
