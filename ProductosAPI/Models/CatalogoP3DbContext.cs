using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductosAPI.Models;

public partial class CatalogoP3DbContext : DbContext
{
    public CatalogoP3DbContext()
    {
    }

    public CatalogoP3DbContext(DbContextOptions<CatalogoP3DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Imagene> Imagenes { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-293A69F\\SQLEXPRESS; Database=CATALOGO_P3_DB; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.ToTable("ARTICULOS");

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("money");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("CATEGORIAS");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Imagene>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("IMAGENES");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.ToTable("MARCAS");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
