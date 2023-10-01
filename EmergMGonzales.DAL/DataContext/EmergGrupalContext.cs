using System;
using System.Collections.Generic;
using EmergMGonzales.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

//Prueba de pull fermin :)
//prueba lucia 
//coen lucia

namespace EmergMGonzales.DAL.DataContext
{
    public partial class EmergGrupalContext : DbContext
    {
        public EmergGrupalContext()
        {
        }

        public EmergGrupalContext(DbContextOptions<EmergGrupalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__5B65BF97C007A1C5");

                entity.ToTable("USUARIO");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
