using Microsoft.EntityFrameworkCore;
using SistemaUniversidad.Models;

namespace SistemaUniversidad.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tablas de la base de datos
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Estudiante> Estudiantes { get; set; }

        public DbSet<Docente> Docentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nombres de las tablas
            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Estudiante>().ToTable("Estudiantes");
            modelBuilder.Entity<Docente>().ToTable("Docentes");

            // La propiedad UsuarioNombre corresponde a la columna Usuario
            modelBuilder.Entity<Usuario>()
                .Property(u => u.UsuarioNombre)
                .HasColumnName("Usuario");

            // Relación Usuario -> Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);

            // Relación Estudiante -> Usuario (1 a 1)
            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.Usuario)
                .WithOne(u => u.Estudiante)
                .HasForeignKey<Estudiante>(e => e.IdUsuario);

            // Relación Docente -> Usuario (1 a 1)
            modelBuilder.Entity<Docente>()
                .HasOne(d => d.Usuario)
                .WithOne(u => u.Docente)
                .HasForeignKey<Docente>(d => d.IdUsuario);
        }
    }
}
