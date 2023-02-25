using Microsoft.EntityFrameworkCore;
namespace DAWpractica1.Models
{
    public class equipos_context : DbContext
    {
        public equipos_context(DbContextOptions<equipos_context> options) : base(options) {
            
        }

        public DbSet<Equipos> equipos { get; set; }
    }
}
