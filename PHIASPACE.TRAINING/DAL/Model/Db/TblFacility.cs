using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIASPACE.TRAINING.DAL.Model.Db
{
    public class TblFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacilityId { get; set; }
        public int ProvinceId { get; set; }
        public TblProvince Province { get; set; }
        public int DistrictId { get; set; }
        public TblDistrict District { get; set; }
        public string FacilityName { get; set; }
        public int? ClassificationId { get; set; }
        public TblFacilityClassification Classification { get; set; }
        public string? UsCode { get; set; }
        public double? Latitude { get; set; }
        public int? LevelId { get; set; }
        public TblFacilityLevel Level { get; set; }
        public double? Longitude { get; set; }
        public int? TypeId { get; set; }
        public TblFacilityType Type { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int? Deleted { get; set; }
    }
}
