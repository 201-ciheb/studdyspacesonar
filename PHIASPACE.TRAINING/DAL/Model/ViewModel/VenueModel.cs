using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PHIASPACE.TRAINING.DAL.Model.ViewModel
{
    public class VenueModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Address { get; set; }
        [Required]
        [Display(Name = "Province/State")]
        public int? ProvinceId { get; set; }
        [Required]
        [Display(Name = "District/LGA")]
        public int? DistrictId { get; set; }
    }
}
