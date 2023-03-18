using Microsoft.EntityFrameworkCore;
namespace DAWpractica1.Models
{
    public class equipos_context : DbContext
    {
        public equipos_context(DbContextOptions<equipos_context> options) : base(options) {
            
        }

        public DbSet<Equipos> equipos { get; set; }

        public DbSet<Equipos> usuarios { get; set; }
        public DbSet<Equipos> tipo_equipo { get; set; }
        public DbSet<Equipos> reservas { get; set; }
        public DbSet<Equipos> marcas { get; set; }
        public DbSet<Equipos> facultades { get; set; }
        public DbSet<Equipos> estados_reserva { get; set; }
        public DbSet<Equipos> estados_equipo { get; set; }
        public DbSet<Equipos> carreras { get; set; }
    }
}
