using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }

        public List<Question> Questions { get; set; }
    }
}
