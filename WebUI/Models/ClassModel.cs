
using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class ClassModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string? Name { get; set; }

        public List<Branch>? SelectedBranch { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }


    }
}
