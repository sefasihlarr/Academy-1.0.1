namespace EntityLayer.Concrete
{
    public class Exam
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public String Description { get; set; }

        public string? ExamDate { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int Timer { get; set; }

        public List<ExamAnswers> ExamAnswers { get; set; }
        public List<ExamQuestions> ExamQuestions { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }

    }
}
