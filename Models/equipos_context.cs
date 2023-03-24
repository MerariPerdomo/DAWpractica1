using Microsoft.EntityFrameworkCore;
namespace DAWpractica1.Models
{
    public class equipos_context : DbContext
    {
        public equipos_context(DbContextOptions<equipos_context> options) : base(options) {
            
        }

        public DbSet<Equipos> equipos { get; set; }

        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<tipo_equipo> tipo_equipo { get; set; }
        public DbSet<reservas> reservas { get; set; }
        public DbSet<marcas> marcas { get; set; }
        public DbSet<facultades> facultades { get; set; }
        public DbSet<estados_reserva> estados_reserva { get; set; }
        public DbSet<estados_equipo> estados_equipo { get; set; }
        public DbSet<carreras> carreras { get; set; }
    }
}
