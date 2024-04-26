using EntityLayer.Concrete;
using Microsoft.Build.Framework;

namespace WebUI.Models
{
    public class ExamModel
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string Description { get; set; }
        public string ExamDate { get; set; }

        public int Timer { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int UserId { get; set; }
        public AppUser? User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }

        public List<Question> Questions { get; set; }


    }
}
