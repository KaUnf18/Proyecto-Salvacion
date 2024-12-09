using Proyecto_Salvacion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Proyecto_Salvacion.Data
{
    public class SalonComunalContext : DbContext
    {
        public SalonComunalContext(DbContextOptions<SalonComunalContext> options) : base(options) { }

        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ReservaProducto> ReservaProductos { get; set; }
        public DbSet<Rol> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define la clave primaria compuesta en ReservaProducto
            modelBuilder.Entity<ReservaProducto>()
                .HasKey(rp => new { rp.ReservaId, rp.ProductoId });

            modelBuilder.Entity<ReservaProducto>()
                .HasOne(rp => rp.Reserva)
                .WithMany(r => r.ReservaProductos)
                .HasForeignKey(rp => rp.ReservaId);

            modelBuilder.Entity<ReservaProducto>()
                .HasOne(rp => rp.Producto)
                .WithMany(p => p.ReservaProductos)
                .HasForeignKey(rp => rp.ProductoId);
        }
    }

}
