using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PHIASPACE.LINKAGE.DAL.Model;

public partial class AimsDbContext : DbContext
{
    public AimsDbContext()
    {
    }

    public AimsDbContext(DbContextOptions<AimsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AimsRecordSet> AimsRecordSets { get; set; }

    public virtual DbSet<AimsValueSet> AimsValueSets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AimsRecordSet>(entity =>
        {
            entity.ToTable("aims_record_set");

            entity.Property(e => e.BaseQuery).HasMaxLength(50);
            entity.Property(e => e.Label).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<AimsValueSet>(entity =>
        {
            entity.ToTable("aims_value_set");

            entity.Property(e => e.CountParameter).HasMaxLength(50);
            entity.Property(e => e.ItemName).HasMaxLength(20);
            entity.Property(e => e.Label).HasMaxLength(70);
            entity.Property(e => e.SpName).HasMaxLength(60);
            entity.Property(e => e.TableName).HasMaxLength(30);
            entity.Property(e => e.Value).HasMaxLength(60);
            entity.Property(e => e.ValuesetType).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
