using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }

        public string QuestionText { get; set; }

        //public int LessonId { get; set; }
        //public Lesson Lesson { get; set; }


        public int LevelId { get; set; }
        public Level Level { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public List<Option> Options { get; set; }

        public int OutputId { get; set; }
        public Output Output { get; set; }

        public int UserId { get; set; }
        public AppUser? User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }
        public bool SolutionCondition { get; set; }
    }
}
