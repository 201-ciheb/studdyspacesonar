using Microsoft.EntityFrameworkCore;
using PHIASPACE.TRAINING.DAL.Model.Db;
//using Microsoft.EntityFrameworkCore.DbContext;

namespace PHIASPACE.TRAINING.DAL.Model
{
    public class PhiaSpaceTrainingContext : DbContext
    {
        public PhiaSpaceTrainingContext(DbContextOptions<PhiaSpaceTrainingContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public virtual DbSet<TblPersons> Tbl_Persons { get; set; }
        public virtual DbSet<TblCertificate> Tbl_Certificate { get; set; }
        public virtual DbSet<TblCourse> Tbl_Course { get; set; }
        public virtual DbSet<TblDistrict> Tbl_District { get; set; }
        public virtual DbSet<TblFacility> Tbl_Facility { get; set; }
        public virtual DbSet<TblFacilityClassification> Tbl_FacilityClassification { get; set; }
        public virtual DbSet<TblFacilityLevel> Tbl_FacilityLevel { get; set; }
        public virtual DbSet<TblFacilityType> Tbl_FacilityType { get; set; }
        public virtual DbSet<TblFacilityTransfers> Tbl_FacilityTransfers { get; set; }
        public virtual DbSet<TblLookup> Tbl_Lookup { get; set; }
        public virtual DbSet<TblProfession> Tbl_Profession { get; set; }
        public virtual DbSet<TblProvince> Tbl_Province { get; set; }
        public virtual DbSet<TblTraining> Tbl_Training { get; set; }
        public virtual DbSet<TblTrainingFacilitator> Tbl_TrainingFacilitator { get; set; }
        public virtual DbSet<TblTrainingParticipant> Tbl_TrainingParticipant{ get; set; }
        public virtual DbSet<TblTrainingVenue> Tbl_Venue { get; set; }
        public virtual DbSet<TblThematicArea> Tbl_ThematicArea { get; set; }
    }
}
