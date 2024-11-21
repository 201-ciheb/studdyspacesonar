using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.INCIDENCE.DAL.Model;

namespace PHIASPACE.INCIDENCE.DAL.MysqlDbService;

public partial class AimsMysqlDbContext : DbContext
{
    public AimsMysqlDbContext()
    {
    }

    public AimsMysqlDbContext(DbContextOptions<AimsMysqlDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AimsCountry> AimsCountries { get; set; }

    public virtual DbSet<AimsCounty> AimsCounties { get; set; }

    public virtual DbSet<AimsLnkTeamPerson> AimsLnkTeamPeople { get; set; }

    public virtual DbSet<AimsLookup> AimsLookups { get; set; }

    public virtual DbSet<AimsPerson> AimsPeople { get; set; }

    public virtual DbSet<AimsRecordSet> AimsRecordSets { get; set; }

    public virtual DbSet<AimsValueSet> AimsValueSets { get; set; }

    public virtual DbSet<AimsZone> AimsZones { get; set; }

    public virtual DbSet<FieldCheck> FieldChecks { get; set; }

    public virtual DbSet<GetEaDetailsTable> GetEaDetailsTables { get; set; }

    public virtual DbSet<GetEaHouseholdSummary> GetEaHouseholdSummaries { get; set; }

    public virtual DbSet<LbLab> LbLabs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AimsCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aims_country");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
        });

        modelBuilder.Entity<AimsCounty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aims_county");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountyCode)
                .HasMaxLength(50)
                .HasColumnName("county_code");
            entity.Property(e => e.CountyName)
                .HasMaxLength(250)
                .HasColumnName("county_name");
            entity.Property(e => e.GeoPolicticalRegion)
                .HasMaxLength(50)
                .HasColumnName("geo_polictical_region");
        });

        modelBuilder.Entity<AimsLnkTeamPerson>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("aims_lnk_team_person");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(6)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TmRole)
                .HasMaxLength(50)
                .HasColumnName("tm_role");
        });

        modelBuilder.Entity<AimsLookup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("aims_lookup");

            entity.Property(e => e.Category)
                .HasMaxLength(200)
                .HasColumnName("category");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LookupName)
                .HasMaxLength(200)
                .HasColumnName("lookup_name");
            entity.Property(e => e.Orderd).HasColumnName("orderd");
        });

        modelBuilder.Entity<AimsPerson>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("aims_person");

            entity.Property(e => e.CreatedAt)
                .HasMaxLength(6)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.NationCode)
                .HasMaxLength(10)
                .HasColumnName("nation_code");
            entity.Property(e => e.Organization).HasColumnName("organization");
            entity.Property(e => e.OtherNames)
                .HasMaxLength(50)
                .HasColumnName("other_names");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TagNumber)
                .HasMaxLength(20)
                .HasColumnName("tag_number");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<AimsRecordSet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("aims_record_set");

            entity.Property(e => e.BaseQuery).HasMaxLength(50);
            entity.Property(e => e.Label).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<AimsValueSet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("aims_value_set");

            entity.Property(e => e.CountParameter).HasMaxLength(50);
            entity.Property(e => e.ItemName).HasMaxLength(20);
            entity.Property(e => e.Label).HasMaxLength(70);
            entity.Property(e => e.SpName).HasMaxLength(60);
            entity.Property(e => e.TableName).HasMaxLength(30);
            entity.Property(e => e.Value).HasMaxLength(60);
            entity.Property(e => e.ValuesetType).HasMaxLength(10);
        });

        modelBuilder.Entity<AimsZone>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("aims_zone");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FieldCheck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("field_check");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Graph)
                .HasColumnType("text")
                .HasColumnName("graph");
            entity.Property(e => e.TableDetail)
                .HasColumnType("text")
                .HasColumnName("table_detail");
            entity.Property(e => e.TableHeader)
                .HasColumnType("text")
                .HasColumnName("table_header");
            entity.Property(e => e.TableId)
                .HasMaxLength(255)
                .HasColumnName("table_id");
            entity.Property(e => e.TableJson)
                .HasColumnType("json")
                .HasColumnName("table_json");
            entity.Property(e => e.TableName)
                .HasMaxLength(255)
                .HasColumnName("table_name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<GetEaDetailsTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("get_ea_details_table");

            entity.Property(e => e.Ycluster)
                .HasPrecision(4)
                .HasColumnName("ycluster");
            entity.Property(e => e.Ycoutyn)
                .HasColumnName("ycoutyn")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Ydivisionn)
                .HasColumnName("ydivisionn")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Ylocationn)
                .HasColumnName("ylocationn")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Ysubcountyn)
                .HasColumnName("ysubcountyn")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<GetEaHouseholdSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("get_ea_household_summary");

            entity.Property(e => e.ClusterNumber)
                .HasPrecision(4)
                .HasColumnName("cluster_number");
            entity.Property(e => e.HouseholdsCompleted).HasColumnName("Households_Completed");
            entity.Property(e => e.HouseholdsRefused).HasColumnName("Households_Refused");
            entity.Property(e => e.HouseholdsVisited)
                .HasPrecision(23)
                .HasColumnName("Households_Visited");
        });

        modelBuilder.Entity<LbLab>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lb_lab");

            entity.Property(e => e.Id)
                .HasMaxLength(64)
                .HasColumnName("id");
            entity.Property(e => e.CountyCode)
                .HasMaxLength(5)
                .HasColumnName("county_code");
            entity.Property(e => e.DeleteFlag).HasColumnName("delete_flag");
            entity.Property(e => e.KenphiaCode)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("kenphia_code");
            entity.Property(e => e.LabName)
                .HasMaxLength(250)
                .HasColumnName("lab_name");
            entity.Property(e => e.Ldms)
                .HasMaxLength(5)
                .HasColumnName("ldms");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
