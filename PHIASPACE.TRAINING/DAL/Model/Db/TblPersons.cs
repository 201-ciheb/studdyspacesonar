using System.ComponentModel.DataAnnotations;

namespace PHIASPACE.TRAINING.DAL.Model.Db
{
    public class TblPersons
    {
        [Key]
        public string IdentificationCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public int? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Title { get; set; }
        public int? ProvinceId { get; set; }
        public TblProvince Province { get; set; }
        public int? DistrictId { get; set; }
        public TblDistrict District { get; set; }
        public int? FacilityId { get; set; }
        public TblFacility TblFacility { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? ProfessionId { get; set; }
        public TblProfession Profession { get; set; }

        //Facilitator
        private int? FacilitatorType { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int? Deleted { get; set; }
    }
}
