using Microsoft.AspNetCore.Identity;

namespace PHIASPACE.CORE.DAL.Model
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class PhiaSpaceUser : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string? AddressLevel1 { get; set; }
        public string? AddressLevel2 { get; set; }
        public string? City { get; set; }
        public string? Qualification {get;set;} 
        public string Gender {get; set;}      
        public int Deleted {get;set;}
    }
}
