using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Models;

public partial class DbcrudBlazorContext : DbContext
{
    public DbcrudBlazorContext()
    {
    }

    public DbcrudBlazorContext(DbContextOptions<DbcrudBlazorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity => 
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__2EC8D2AC231970F0");

            entity.ToTable("Persona");

            entity.Property(e => e.Nombres)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Apellidos)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Foto)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.EnteredDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
