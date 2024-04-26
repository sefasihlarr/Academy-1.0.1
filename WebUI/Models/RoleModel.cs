using Microsoft.Build.Framework;

namespace WebUI.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }
    }
}
