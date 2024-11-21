using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.INCIDENCE.DAL.Model;

namespace PHIASPACE.INCIDENCE.DAL.MssqlDbService;

public partial class AimsMssqlDbContext : DbContext
{
    public AimsMssqlDbContext()
    {
    }

    public AimsMssqlDbContext(DbContextOptions<AimsMssqlDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AimsRecordSet> AimsRecordSets { get; set; }
    public DbSet<AimsCountry> AimsCountries { get; set; }
    public DbSet<AimsCounty> AimsCountys { get; set; }
    public DbSet<AimsZone> AimsZones { get; set; }
    public DbSet<AimsPerson> AimsPersons { get; set; }
    public DbSet<AimsLookup> AimsLookups { get; set; }
    public DbSet<AimsLnkTeamPerson> AimsLnkTeamPersons { get; set; }
    public DbSet<LbLab> LbLabs { get; set; }

    public virtual DbSet<AimsValueSet> AimsValueSets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration for AimsRecordSet
        modelBuilder.Entity<AimsRecordSet>(entity =>
        {
            entity.ToTable("aims_record_set");
            entity.Property(e => e.BaseQuery).HasMaxLength(50);
            entity.Property(e => e.Label).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(25);
        });

            // Configure AimsValueSet
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

        // Add additional relationships and configurations as necessary
        OnModelCreatingPartial(modelBuilder);

    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
