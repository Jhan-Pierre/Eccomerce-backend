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

    public virtual DbSet<TbModule> TbModules { get; set; }

    public virtual DbSet<TbPermission> TbPermissions { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbRolePermission> TbRolePermissions { get; set; }

    public virtual DbSet<TbShift> TbShifts { get; set; }

    public virtual DbSet<TbState> TbStates { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_modul__3213E83F43A81771");

            entity.ToTable("tb_module");

            entity.HasIndex(e => e.Name, "UQ__tb_modul__72E12F1B76F214DE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_permi__3213E83F88405BCA");

            entity.ToTable("tb_permission");

            entity.HasIndex(e => e.Name, "UQ__tb_permi__72E12F1B34CF7E69").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.Module).WithMany(p => p.TbPermissions)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__tb_permis__modul__440B1D61");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_role__3213E83F7E51A857");

            entity.ToTable("tb_role");

            entity.HasIndex(e => e.Name, "UQ__tb_role__72E12F1B45DD89E8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbRolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_role___3213E83FDD340459");

            entity.ToTable("tb_role_permission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.TbRolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK__tb_role_p__permi__47DBAE45");

            entity.HasOne(d => d.Role).WithMany(p => p.TbRolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__tb_role_p__role___46E78A0C");
        });

        modelBuilder.Entity<TbShift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_shift__3213E83F10078CE2");

            entity.ToTable("tb_shift");

            entity.HasIndex(e => e.Name, "UQ__tb_shift__72E12F1BF47B25B9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_state__3213E83F91740684");

            entity.ToTable("tb_state");

            entity.HasIndex(e => e.Name, "UQ__tb_state__72E12F1B269BE59F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_user__3213E83F88F1299A");

            entity.ToTable("tb_user");

            entity.HasIndex(e => e.Email, "UQ__tb_user__AB6E61649E1D72D7").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__tb_user__B43B145FEE1267BE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.ShiftId).HasColumnName("shift_id");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.TbUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_user__role_id__4CA06362");

            entity.HasOne(d => d.Shift).WithMany(p => p.TbUsers)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK__tb_user__shift_i__4E88ABD4");

            entity.HasOne(d => d.State).WithMany(p => p.TbUsers)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_user__state_i__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
