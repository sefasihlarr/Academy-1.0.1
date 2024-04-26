using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class LessonModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public int UserId { get; set; }
        public virtual AppUser? User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }
    }
}
