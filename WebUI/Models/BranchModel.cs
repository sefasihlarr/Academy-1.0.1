using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class BranchModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "En az 1 karakter içermelidir ")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "En az 1 karakter içermelidir ")]
        public string? Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }
    }
}
