using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task2.Data.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Relative> Relatives { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=182.93.94.30;Database=EMS;User=sa;Password=bdnquery;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepId).HasName("PK__Departme__DB9CAA5F019735ED");

            entity.ToTable("Department");

            entity.Property(e => e.DepName).HasMaxLength(90);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => e.Did).HasName("PK__Designat__C0312218CCB549B5");

            entity.ToTable("Designation");

            entity.Property(e => e.Dname)
                .HasMaxLength(90)
                .HasColumnName("DName");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Eid).HasName("PK__Employee__C1971B537FA25ADB");

            entity.ToTable("Employee");

            entity.Property(e => e.DepId).HasColumnName("DepID");
            entity.Property(e => e.Email).HasMaxLength(90);
            entity.Property(e => e.Name).HasMaxLength(90);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.Dep).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepId)
                .HasConstraintName("FK__Employee__DepID__286302EC");

            entity.HasOne(d => d.DidNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Did)
                .HasConstraintName("FK__Employee__Did__29572725");
        });

        modelBuilder.Entity<Relative>(entity =>
        {
            entity.HasKey(e => e.Rid).HasName("PK__Relative__CAF055CA065AD8FF");

            entity.ToTable("Relative");

            entity.Property(e => e.Email).HasMaxLength(90);
            entity.Property(e => e.Name).HasMaxLength(90);
            entity.Property(e => e.Phone).HasMaxLength(90);
            entity.Property(e => e.Relation).HasMaxLength(90);

            entity.HasOne(d => d.EidNavigation).WithMany(p => p.Relatives)
                .HasForeignKey(d => d.Eid)
                .HasConstraintName("FK__Relative__Eid__31EC6D26");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
