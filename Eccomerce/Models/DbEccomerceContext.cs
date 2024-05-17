using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eccomerce.Models;

public partial class DbEccomerceContext : DbContext
{
    public DbEccomerceContext()
    {
    }

    public DbEccomerceContext(DbContextOptions<DbEccomerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbState> TbStates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_state__3213E83F0CDDF71F");

            entity.ToTable("tb_state");

            entity.HasIndex(e => e.Name, "UQ__tb_state__72E12F1BABC719CA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
