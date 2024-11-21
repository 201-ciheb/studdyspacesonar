using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PHIASPACE.MAPPINGLISTING.DAL.Model.Db;

public partial class ZamphiaMainDbContext : DbContext
{
    public ZamphiaMainDbContext()
    {
    }

    public ZamphiaMainDbContext(DbContextOptions<ZamphiaMainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Zamphia2020EainforRec> Zamphia2020EainforRecs { get; set; }

    public virtual DbSet<Zamphia2020Record1> Zamphia2020Record1s { get; set; }

    public virtual DbSet<Zamphia2020Record2> Zamphia2020Record2s { get; set; }

    public virtual DbSet<Zamphia2020Segrec1> Zamphia2020Segrec1s { get; set; }

    public virtual DbSet<Zamphia2020Segrec2> Zamphia2020Segrec2s { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4SN38VM\\SQLEXPRESS;Initial Catalog=ZAMPHIA_2020_MAIN_DB;Persist Security Info=False;User ID=sa;Password=P@ssw0rd;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=500;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Zamphia2020EainforRec>(entity =>
        {
            entity.HasKey(e => e.CaseNumber).HasName("PK__ZAMPHIA2__103BB8D9E6FB5971");

            entity.ToTable("ZAMPHIA2020_EAINFOR_REC");

            entity.Property(e => e.CaseNumber).HasMaxLength(256);
            entity.Property(e => e.Accomodation)
                .HasMaxLength(256)
                .HasColumnName("ACCOMODATION");
            entity.Property(e => e.Altnet)
                .HasMaxLength(256)
                .HasColumnName("ALTNET");
            entity.Property(e => e.Bestnet)
                .HasMaxLength(256)
                .HasColumnName("BESTNET");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EainforId)
                .HasMaxLength(256)
                .HasColumnName("EAINFOR_ID");
            entity.Property(e => e.Electricity)
                .HasMaxLength(256)
                .HasColumnName("ELECTRICITY");
            entity.Property(e => e.FileName).HasMaxLength(256);
            entity.Property(e => e.Floodrisk)
                .HasMaxLength(256)
                .HasColumnName("FLOODRISK");
            entity.Property(e => e.Languages)
                .HasMaxLength(256)
                .HasColumnName("LANGUAGES");
            entity.Property(e => e.Midnet)
                .HasMaxLength(256)
                .HasColumnName("MIDNET");
            entity.Property(e => e.NetworkRank)
                .HasMaxLength(256)
                .HasColumnName("NETWORK_RANK");
            entity.Property(e => e.Notes)
                .HasMaxLength(256)
                .HasColumnName("NOTES");
            entity.Property(e => e.Otherlang)
                .HasMaxLength(256)
                .HasColumnName("OTHERLANG");
            entity.Property(e => e.Police)
                .HasMaxLength(256)
                .HasColumnName("POLICE");
            entity.Property(e => e.Roads)
                .HasMaxLength(256)
                .HasColumnName("ROADS");
            entity.Property(e => e.TeamLeadid)
                .HasMaxLength(256)
                .HasColumnName("TEAM_LEADID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Worstnet)
                .HasMaxLength(256)
                .HasColumnName("WORSTNET");
        });

        modelBuilder.Entity<Zamphia2020Record1>(entity =>
        {
            entity.HasKey(e => e.CaseNumber).HasName("PK__ZAMPHIA2__103BB8D91AAC6E07");

            entity.ToTable("ZAMPHIA2020_RECORD1");

            entity.Property(e => e.CaseNumber).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(256);
            entity.Property(e => e.Lcluster)
                .HasMaxLength(256)
                .HasColumnName("LCLUSTER");
            entity.Property(e => e.Lconst)
                .HasMaxLength(256)
                .HasColumnName("LCONST");
            entity.Property(e => e.Ldate)
                .HasMaxLength(256)
                .HasColumnName("LDATE");
            entity.Property(e => e.Ldistrict)
                .HasMaxLength(256)
                .HasColumnName("LDISTRICT");
            entity.Property(e => e.Lexpect)
                .HasMaxLength(256)
                .HasColumnName("LEXPECT");
            entity.Property(e => e.Lhouseholds)
                .HasMaxLength(256)
                .HasColumnName("LHOUSEHOLDS");
            entity.Property(e => e.Lprovince)
                .HasMaxLength(256)
                .HasColumnName("LPROVINCE");
            entity.Property(e => e.Lsegnum)
                .HasMaxLength(256)
                .HasColumnName("LSEGNUM");
            entity.Property(e => e.Ltleadnum)
                .HasMaxLength(256)
                .HasColumnName("LTLEADNUM");
            entity.Property(e => e.Ltype)
                .HasMaxLength(256)
                .HasColumnName("LTYPE");
            entity.Property(e => e.Lward)
                .HasMaxLength(256)
                .HasColumnName("LWARD");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Zamphia2020Record2>(entity =>
        {
            entity.HasKey(e => e.CaseNumber).HasName("PK__ZAMPHIA2__103BB8D9BA7FFBB6");

            entity.ToTable("ZAMPHIA2020_RECORD2");

            entity.Property(e => e.CaseNumber).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(256);
            entity.Property(e => e.Laccuracy)
                .HasMaxLength(256)
                .HasColumnName("LACCURACY");
            entity.Property(e => e.Laddress)
                .HasMaxLength(256)
                .HasColumnName("LADDRESS");
            entity.Property(e => e.Laltitude)
                .HasMaxLength(256)
                .HasColumnName("LALTITUDE");
            entity.Property(e => e.Lcluster)
                .HasMaxLength(256)
                .HasColumnName("LCLUSTER");
            entity.Property(e => e.Lgps)
                .HasMaxLength(256)
                .HasColumnName("LGPS");
            entity.Property(e => e.Lhhoccstat)
                .HasMaxLength(256)
                .HasColumnName("LHHOCCSTAT");
            entity.Property(e => e.Lhouse)
                .HasMaxLength(256)
                .HasColumnName("LHOUSE");
            entity.Property(e => e.Lintnum)
                .HasMaxLength(256)
                .HasColumnName("LINTNUM");
            entity.Property(e => e.Lintro)
                .HasMaxLength(256)
                .HasColumnName("LINTRO");
            entity.Property(e => e.Llang)
                .HasMaxLength(256)
                .HasColumnName("LLANG");
            entity.Property(e => e.Llangoth)
                .HasMaxLength(256)
                .HasColumnName("LLANGOTH");
            entity.Property(e => e.Llatitude)
                .HasMaxLength(256)
                .HasColumnName("LLATITUDE");
            entity.Property(e => e.Llongitude)
                .HasMaxLength(256)
                .HasColumnName("LLONGITUDE");
            entity.Property(e => e.Lname)
                .HasMaxLength(256)
                .HasColumnName("LNAME");
            entity.Property(e => e.Lnickname)
                .HasMaxLength(256)
                .HasColumnName("LNICKNAME");
            entity.Property(e => e.Lnotes)
                .HasMaxLength(256)
                .HasColumnName("LNOTES");
            entity.Property(e => e.Lnumber)
                .HasMaxLength(256)
                .HasColumnName("LNUMBER");
            entity.Property(e => e.Lresidence)
                .HasMaxLength(256)
                .HasColumnName("LRESIDENCE");
            entity.Property(e => e.Lsatellit)
                .HasMaxLength(256)
                .HasColumnName("LSATELLIT");
            entity.Property(e => e.Lselect)
                .HasMaxLength(256)
                .HasColumnName("LSELECT");
            entity.Property(e => e.Lstruct)
                .HasMaxLength(256)
                .HasColumnName("LSTRUCT");
            entity.Property(e => e.Ltleadnum)
                .HasMaxLength(256)
                .HasColumnName("LTLEADNUM");
            entity.Property(e => e.Ltruehh)
                .HasMaxLength(256)
                .HasColumnName("LTRUEHH");
            entity.Property(e => e.Ltruehhoth)
                .HasMaxLength(256)
                .HasColumnName("LTRUEHHOTH");
            entity.Property(e => e.Lunit)
                .HasMaxLength(256)
                .HasColumnName("LUNIT");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Zamphia2020Segrec1>(entity =>
        {
            entity.HasKey(e => e.CaseNumber).HasName("PK__ZAMPHIA2__103BB8D986CD14FB");

            entity.ToTable("ZAMPHIA2020_SEGREC1");

            entity.Property(e => e.CaseNumber).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(256);
            entity.Property(e => e.Sauxiliar)
                .HasMaxLength(256)
                .HasColumnName("SAUXILIAR");
            entity.Property(e => e.Scluster)
                .HasMaxLength(256)
                .HasColumnName("SCLUSTER");
            entity.Property(e => e.Sfinal)
                .HasMaxLength(256)
                .HasColumnName("SFINAL");
            entity.Property(e => e.Soption)
                .HasMaxLength(256)
                .HasColumnName("SOPTION");
            entity.Property(e => e.Sseghh)
                .HasMaxLength(256)
                .HasColumnName("SSEGHH");
            entity.Property(e => e.Ssegnum)
                .HasMaxLength(256)
                .HasColumnName("SSEGNUM");
            entity.Property(e => e.Stothh)
                .HasMaxLength(256)
                .HasColumnName("STOTHH");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Zamphia2020Segrec2>(entity =>
        {
            entity.HasKey(e => e.CaseNumber).HasName("PK__ZAMPHIA2__103BB8D98E37E8A1");

            entity.ToTable("ZAMPHIA2020_SEGREC2");

            entity.Property(e => e.CaseNumber).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(256);
            entity.Property(e => e.Scluster)
                .HasMaxLength(256)
                .HasColumnName("SCLUSTER");
            entity.Property(e => e.Scummul)
                .HasMaxLength(256)
                .HasColumnName("SCUMMUL");
            entity.Property(e => e.Segnum)
                .HasMaxLength(256)
                .HasColumnName("SEGNUM");
            entity.Property(e => e.Shhnumb)
                .HasMaxLength(256)
                .HasColumnName("SHHNUMB");
            entity.Property(e => e.Smore)
                .HasMaxLength(256)
                .HasColumnName("SMORE");
            entity.Property(e => e.Spercent)
                .HasMaxLength(256)
                .HasColumnName("SPERCENT");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
