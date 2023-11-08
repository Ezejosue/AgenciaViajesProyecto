using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AgenciaViajes.Models;

public partial class AgenciaViajesContext : DbContext
{
    public AgenciaViajesContext()
    {
    }

    public AgenciaViajesContext(DbContextOptions<AgenciaViajesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividade> Actividades { get; set; }

    public virtual DbSet<Busqueda> Busquedas { get; set; }

    public virtual DbSet<Destino> Destinos { get; set; }

    public virtual DbSet<DestinosAleatorio> DestinosAleatorios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividade>(entity =>
        {
            entity.HasKey(e => e.ActividadId).HasName("PK__Activida__981483F09277E758");

            entity.Property(e => e.ActividadId).HasColumnName("ActividadID");
            entity.Property(e => e.DestinoId).HasColumnName("DestinoID");
            entity.Property(e => e.Duracion).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Destino).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.DestinoId)
                .HasConstraintName("FK__Actividad__Desti__286302EC");
        });

        modelBuilder.Entity<Busqueda>(entity =>
        {
            entity.HasKey(e => e.BusquedaId).HasName("PK__Busqueda__AC2485877106A15C");

            entity.Property(e => e.BusquedaId).HasColumnName("BusquedaID");
            entity.Property(e => e.FechaBusqueda).HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Busqueda)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Busquedas__Usuar__2B3F6F97");

            entity.HasMany(d => d.Destinos).WithMany(p => p.Busqueda)
                .UsingEntity<Dictionary<string, object>>(
                    "BusquedasDestino",
                    r => r.HasOne<Destino>().WithMany()
                        .HasForeignKey("DestinoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Busquedas__Desti__2F10007B"),
                    l => l.HasOne<Busqueda>().WithMany()
                        .HasForeignKey("BusquedaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Busquedas__Busqu__2E1BDC42"),
                    j =>
                    {
                        j.HasKey("BusquedaId", "DestinoId").HasName("PK__Busqueda__D88CBD68079EEED1");
                        j.ToTable("Busquedas_Destinos");
                        j.IndexerProperty<int>("BusquedaId").HasColumnName("BusquedaID");
                        j.IndexerProperty<int>("DestinoId").HasColumnName("DestinoID");
                    });
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.HasKey(e => e.DestinoId).HasName("PK__Destinos__4A838EF659BCD489");

            entity.Property(e => e.DestinoId).HasColumnName("DestinoID");
            entity.Property(e => e.ImagenUrl).HasColumnName("ImagenURL");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Pais).HasMaxLength(100);
            entity.Property(e => e.ZonaGeografica).HasMaxLength(100);
        });

        modelBuilder.Entity<DestinosAleatorio>(entity =>
        {
            entity.HasKey(e => e.DestinoAleatorioId).HasName("PK__Destinos__C45F3B66BBA89815");

            entity.ToTable("Destinos_Aleatorios");

            entity.Property(e => e.DestinoAleatorioId).HasColumnName("DestinoAleatorioID");
            entity.Property(e => e.DestinoId).HasColumnName("DestinoID");
            entity.Property(e => e.FechaGeneracion).HasColumnType("datetime");

            entity.HasOne(d => d.Destino).WithMany(p => p.DestinosAleatorios)
                .HasForeignKey(d => d.DestinoId)
                .HasConstraintName("FK__Destinos___Desti__31EC6D26");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7980738C3A2");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.TipoUsuario).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
