using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Data;

public partial class G6DataBaseFirstContext : DbContext
{
    public G6DataBaseFirstContext()
    {
    }

    public G6DataBaseFirstContext(DbContextOptions<G6DataBaseFirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<NewTable> NewTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D59466420A9EFB60");

            entity.Property(e => e.Genero).HasDefaultValue("");
        });

        modelBuilder.Entity<NewTable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("TablaNueva");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
