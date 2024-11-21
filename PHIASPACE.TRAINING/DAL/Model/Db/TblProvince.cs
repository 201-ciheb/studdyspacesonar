using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIASPACE.TRAINING.DAL.Model.Db
{
    public class TblProvince
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string? MapCode { get; set; }
        public string? Region { get; set; }
        public string? Capital { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public int? Deleted{ get; set; }

        public virtual ICollection<TblDistrict> TblDistricts { get; set; }
    }
}
