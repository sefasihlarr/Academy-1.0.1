using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class OutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public AppUser? User { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }
    }
}
