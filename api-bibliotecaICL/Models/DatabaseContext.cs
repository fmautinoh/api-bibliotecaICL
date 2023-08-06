using System;
using System.Collections.Generic;
using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Inventariobiblioteca.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<EstadoConservacion> EstadoConservacions { get; set; }

    public virtual DbSet<InventarioLibro> InventarioLibros { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<LibrosAutore> LibrosAutores { get; set; }

    public virtual DbSet<TipoAutor> TipoAutors { get; set; }

    public virtual DbSet<TipoLibro> TipoLibros { get; set; }

    public virtual DbSet<VInventario> VInventarios { get; set; }

    public virtual DbSet<VLibro> VLibros { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PK__Autores__F58AE9096C55B09C");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.TipoAutorId).HasColumnName("TipoAutorID");

            entity.HasOne(d => d.TipoAutor).WithMany(p => p.Autores)
                .HasForeignKey(d => d.TipoAutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Autores__TipoAut__3D5E1FD2");
        });

        modelBuilder.Entity<EstadoConservacion>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("PK__EstadoCo__FEF86B60A90E4237");

            entity.ToTable("EstadoConservacion");

            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        modelBuilder.Entity<InventarioLibro>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24B748E08D93");

            entity.ToTable("InventarioLibro");

            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.Codigo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");

            entity.HasOne(d => d.Estado).WithMany(p => p.InventarioLibros)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__Estad__4F7CD00D");

            entity.HasOne(d => d.Libro).WithMany(p => p.InventarioLibros)
                .HasForeignKey(d => d.LibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__Libro__5070F446");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.LibroId).HasName("PK__Libros__35A1EC8D7958B7AB");

            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.Año)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Editorial)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.NombreLib)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.TipoId).HasColumnName("TipoID");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Libros)
                .HasForeignKey(d => d.TipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Libros__TipoID__49C3F6B7");
        });

        modelBuilder.Entity<LibrosAutore>(entity =>
        {
            modelBuilder.Entity<LibrosAutore>()
                .HasKey(e => e.LibroAutorID).HasName("LibrosAutores_pk");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");

            entity.HasOne(d => d.Autor).WithMany()
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LibrosAut__Autor__4CA06362");

            entity.HasOne(d => d.Libro).WithMany()
                .HasForeignKey(d => d.LibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LibrosAut__Libro__4BAC3F29");
        });

        modelBuilder.Entity<TipoAutor>(entity =>
        {
            entity.HasKey(e => e.TipoAutorId).HasName("PK__TipoAuto__39C500C338ACC518");

            entity.ToTable("TipoAutor");

            entity.Property(e => e.TipoAutorId).HasColumnName("TipoAutorID");
            entity.Property(e => e.TipoAutor1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("TipoAutor");
        });

        modelBuilder.Entity<TipoLibro>(entity =>
        {
            entity.HasKey(e => e.TipoLibroId).HasName("PK__TipoLibr__D5FDC1D557A9AB22");

            entity.ToTable("TipoLibro");

            entity.Property(e => e.TipoLibroId).HasColumnName("TipoLibroID");
            entity.Property(e => e.TipoLibro1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TipoLibro");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B85AF4C225");

            entity.ToTable("Usuario");

            entity.Property(e => e.Pwsd)
                .HasColumnType("nvarchar")
                .HasColumnName("pwsd");
            entity.Property(e => e.Usu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usu");
        });

        modelBuilder.Entity<VInventario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_inventario");

            entity.Property(e => e.Codigo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        modelBuilder.Entity<VLibro>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_libro");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.Año)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Editorial)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.NombreLib)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.TipoLibro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoLibroId).HasColumnName("TipoLibroID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}