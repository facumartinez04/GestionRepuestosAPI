using GestionRepuestosAPI.Modelos;
using GestionRespuestosAPI.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionRespuestosAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<RepuestoVehiculo> RepuestosVehiculos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<UsuarioRol> UsuariosRoles { get; set; }

        public DbSet<UsuarioPermiso> UsuariosPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de clave compuesta
            modelBuilder.Entity<RepuestoVehiculo>()
                .HasKey(rv => new { rv.RepuestoId, rv.VehiculoId });

            // Relaciones
            modelBuilder.Entity<RepuestoVehiculo>()
                .HasOne(rv => rv.Repuesto)
                .WithMany(r => r.RepuestosVehiculos)
                .HasForeignKey(rv => rv.RepuestoId);

            modelBuilder.Entity<RepuestoVehiculo>()
                .HasOne(rv => rv.Vehiculo)
                .WithMany(v => v.RepuestosVehiculos)
                .HasForeignKey(rv => rv.VehiculoId);

            modelBuilder.Entity<UsuarioPermiso>()
             .HasKey(up => new { up.idUsuario, up.idPermiso });

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.idUsuario, ur.idRol });
        }



    }
}
