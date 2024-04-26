using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Tc { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }

        //Yetkili ayrımının yapılması için
        public bool Authority { get; set; }

        public Nullable<int> ClassId { get; set; }
        public Class? Class { get; set; }

        public Nullable<int> BranchId { get; set; }
        public Branch? Branch { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }
    }
}
