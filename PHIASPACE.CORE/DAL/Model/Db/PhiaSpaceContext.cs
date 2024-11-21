using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class PhiaSpaceContext : DbContext
    {
        public PhiaSpaceContext()
        {
        }

        public PhiaSpaceContext(DbContextOptions<PhiaSpaceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApiResource> ApiResources { get; set; } = null!;
        public virtual DbSet<ApiResourceClaim> ApiResourceClaims { get; set; } = null!;
        public virtual DbSet<ApiResourceProperty> ApiResourceProperties { get; set; } = null!;
        public virtual DbSet<ApiResourceScope> ApiResourceScopes { get; set; } = null!;
        public virtual DbSet<ApiResourceSecret> ApiResourceSecrets { get; set; } = null!;
        public virtual DbSet<ApiScope> ApiScopes { get; set; } = null!;
        public virtual DbSet<ApiScopeClaim> ApiScopeClaims { get; set; } = null!;
        public virtual DbSet<ApiScopeProperty> ApiScopeProperties { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientClaim> ClientClaims { get; set; } = null!;
        public virtual DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; } = null!;
        public virtual DbSet<ClientGrantType> ClientGrantTypes { get; set; } = null!;
        public virtual DbSet<ClientIdPrestriction> ClientIdPrestrictions { get; set; } = null!;
        public virtual DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; } = null!;
        public virtual DbSet<ClientProperty> ClientProperties { get; set; } = null!;
        public virtual DbSet<ClientRedirectUri> ClientRedirectUris { get; set; } = null!;
        public virtual DbSet<ClientScope> ClientScopes { get; set; } = null!;
        public virtual DbSet<ClientSecret> ClientSecrets { get; set; } = null!;
        public virtual DbSet<DeviceCode> DeviceCodes { get; set; } = null!;
        public virtual DbSet<IdentityResource> IdentityResources { get; set; } = null!;
        public virtual DbSet<IdentityResourceClaim> IdentityResourceClaims { get; set; } = null!;
        public virtual DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; } = null!;
        public virtual DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;
        public virtual DbSet<PhiaApp> PhiaApps { get; set; } = null!;
        public virtual DbSet<PhiaPermission> PhiaPermissions { get; set; } = null!;
        public virtual DbSet<PhiaProject> PhiaProjects { get; set; } = null!;
        public virtual DbSet<PhiaUserPermission> PhiaUserPermissions { get; set; } = null!;
        public virtual DbSet<PhiaUserProject> PhiaUserProjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PHIASpace;Persist Security Info=False;User ID=sa;Password=P@ssw0rd;MultipleActiveResultSets=False;TrustServerCertificate=False;Connection Timeout=500;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiResource>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_ApiResources_Name")
                    .IsUnique();

                entity.Property(e => e.AllowedAccessTokenSigningAlgorithms).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ApiResourceClaim>(entity =>
            {
                entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceClaims_ApiResourceId");

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.HasOne(d => d.ApiResource)
                    .WithMany(p => p.ApiResourceClaims)
                    .HasForeignKey(d => d.ApiResourceId);
            });

            modelBuilder.Entity<ApiResourceProperty>(entity =>
            {
                entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceProperties_ApiResourceId");

                entity.Property(e => e.Key).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(2000);

                entity.HasOne(d => d.ApiResource)
                    .WithMany(p => p.ApiResourceProperties)
                    .HasForeignKey(d => d.ApiResourceId);
            });

            modelBuilder.Entity<ApiResourceScope>(entity =>
            {
                entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceScopes_ApiResourceId");

                entity.Property(e => e.Scope).HasMaxLength(200);

                entity.HasOne(d => d.ApiResource)
                    .WithMany(p => p.ApiResourceScopes)
                    .HasForeignKey(d => d.ApiResourceId);
            });

            modelBuilder.Entity<ApiResourceSecret>(entity =>
            {
                entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceSecrets_ApiResourceId");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Type).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(4000);

                entity.HasOne(d => d.ApiResource)
                    .WithMany(p => p.ApiResourceSecrets)
                    .HasForeignKey(d => d.ApiResourceId);
            });

            modelBuilder.Entity<ApiScope>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_ApiScopes_Name")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ApiScopeClaim>(entity =>
            {
                entity.HasIndex(e => e.ScopeId, "IX_ApiScopeClaims_ScopeId");

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.HasOne(d => d.Scope)
                    .WithMany(p => p.ApiScopeClaims)
                    .HasForeignKey(d => d.ScopeId);
            });

            modelBuilder.Entity<ApiScopeProperty>(entity =>
            {
                entity.HasIndex(e => e.ScopeId, "IX_ApiScopeProperties_ScopeId");

                entity.Property(e => e.Key).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(2000);

                entity.HasOne(d => d.Scope)
                    .WithMany(p => p.ApiScopeProperties)
                    .HasForeignKey(d => d.ScopeId);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_Clients_ClientId")
                    .IsUnique();

                entity.Property(e => e.AllowedIdentityTokenSigningAlgorithms).HasMaxLength(100);

                entity.Property(e => e.BackChannelLogoutUri).HasMaxLength(2000);

                entity.Property(e => e.ClientClaimsPrefix).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.ClientName).HasMaxLength(200);

                entity.Property(e => e.ClientUri).HasMaxLength(2000);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FrontChannelLogoutUri).HasMaxLength(2000);

                entity.Property(e => e.LogoUri).HasMaxLength(2000);

                entity.Property(e => e.PairWiseSubjectSalt).HasMaxLength(200);

                entity.Property(e => e.ProtocolType).HasMaxLength(200);

                entity.Property(e => e.UserCodeType).HasMaxLength(100);
            });

            modelBuilder.Entity<ClientClaim>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientClaims_ClientId");

                entity.Property(e => e.Type).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(250);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientClaims)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientCorsOrigin>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientCorsOrigins_ClientId");

                entity.Property(e => e.Origin).HasMaxLength(150);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientCorsOrigins)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientGrantType>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientGrantTypes_ClientId");

                entity.Property(e => e.GrantType).HasMaxLength(250);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientGrantTypes)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientIdPrestriction>(entity =>
            {
                entity.ToTable("ClientIdPRestrictions");

                entity.HasIndex(e => e.ClientId, "IX_ClientIdPRestrictions_ClientId");

                entity.Property(e => e.Provider).HasMaxLength(200);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientIdPrestrictions)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientPostLogoutRedirectUri>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientPostLogoutRedirectUris_ClientId");

                entity.Property(e => e.PostLogoutRedirectUri).HasMaxLength(2000);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientPostLogoutRedirectUris)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientProperty>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientProperties_ClientId");

                entity.Property(e => e.Key).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(2000);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientProperties)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientRedirectUri>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientRedirectUris_ClientId");

                entity.Property(e => e.RedirectUri).HasMaxLength(2000);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientRedirectUris)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientScope>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientScopes_ClientId");

                entity.Property(e => e.Scope).HasMaxLength(200);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientScopes)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientSecret>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientSecrets_ClientId");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Type).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(4000);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientSecrets)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<DeviceCode>(entity =>
            {
                entity.HasKey(e => e.UserCode);

                entity.HasIndex(e => e.DeviceCode1, "IX_DeviceCodes_DeviceCode")
                    .IsUnique();

                entity.HasIndex(e => e.Expiration, "IX_DeviceCodes_Expiration");

                entity.Property(e => e.UserCode).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.DeviceCode1)
                    .HasMaxLength(200)
                    .HasColumnName("DeviceCode");

                entity.Property(e => e.SessionId).HasMaxLength(100);

                entity.Property(e => e.SubjectId).HasMaxLength(200);
            });

            modelBuilder.Entity<IdentityResource>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_IdentityResources_Name")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<IdentityResourceClaim>(entity =>
            {
                entity.HasIndex(e => e.IdentityResourceId, "IX_IdentityResourceClaims_IdentityResourceId");

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.HasOne(d => d.IdentityResource)
                    .WithMany(p => p.IdentityResourceClaims)
                    .HasForeignKey(d => d.IdentityResourceId);
            });

            modelBuilder.Entity<IdentityResourceProperty>(entity =>
            {
                entity.HasIndex(e => e.IdentityResourceId, "IX_IdentityResourceProperties_IdentityResourceId");

                entity.Property(e => e.Key).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(2000);

                entity.HasOne(d => d.IdentityResource)
                    .WithMany(p => p.IdentityResourceProperties)
                    .HasForeignKey(d => d.IdentityResourceId);
            });

            modelBuilder.Entity<PersistedGrant>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Expiration, "IX_PersistedGrants_Expiration");

                entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type }, "IX_PersistedGrants_SubjectId_ClientId_Type");

                entity.HasIndex(e => new { e.SubjectId, e.SessionId, e.Type }, "IX_PersistedGrants_SubjectId_SessionId_Type");

                entity.Property(e => e.Key).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.SessionId).HasMaxLength(100);

                entity.Property(e => e.SubjectId).HasMaxLength(200);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<PhiaApp>(entity =>
            {
                entity.ToTable("PhiaApp");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppDescription)
                    .HasMaxLength(600)
                    .HasColumnName("app_description");

                entity.Property(e => e.AppIconPath).HasColumnName("app_icon_path");

                entity.Property(e => e.AppName)
                    .HasMaxLength(150)
                    .HasColumnName("app_name");

                entity.Property(e => e.AppOrder).HasColumnName("app_order");

                entity.Property(e => e.AppStatus)
                    .HasMaxLength(15)
                    .HasColumnName("app_status");

                entity.Property(e => e.AppUrl)
                    .HasMaxLength(450)
                    .HasColumnName("app_url");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.PhiaApps)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhiaApp_PhiaProject");
            });

            modelBuilder.Entity<PhiaPermission>(entity =>
            {
                entity.ToTable("PhiaPermission");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Permission)
                    .HasMaxLength(100)
                    .HasColumnName("permission");

                entity.Property(e => e.PermissionClient)
                    .HasMaxLength(200)
                    .HasColumnName("permission_client");

                entity.Property(e => e.PermissionModule)
                    .HasMaxLength(100)
                    .HasColumnName("permission_module");

                entity.Property(e => e.PermissionType)
                    .HasMaxLength(50)
                    .HasColumnName("permission_type");
            });

            modelBuilder.Entity<PhiaProject>(entity =>
            {
                entity.ToTable("PhiaProject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.ProjectAbbrev)
                    .HasMaxLength(20)
                    .HasColumnName("project_abbrev");

                entity.Property(e => e.ProjectDescription).HasColumnName("project_description");

                entity.Property(e => e.ProjectLogoPath).HasColumnName("project_logo_path");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .HasColumnName("project_name");

                entity.Property(e => e.ProjectStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("project_start_date");
            });

            modelBuilder.Entity<PhiaUserPermission>(entity =>
            {
                entity.ToTable("PhiaUserPermission");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Permission)
                    .HasMaxLength(100)
                    .HasColumnName("permission");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.PermissionNavigation)
                    .WithMany(p => p.PhiaUserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_PhiaUserPermission_PhiaPermission");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PhiaUserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PhiaUserPermission_AspNetUsers");
            });

            modelBuilder.Entity<PhiaUserProject>(entity =>
            {
                entity.ToTable("PhiaUserProject");

                entity.Property(e => e.Default).HasColumnName("default");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.PhiaUserProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhiaUserProject_PhiaProject");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PhiaUserProjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhiaUserProject_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
