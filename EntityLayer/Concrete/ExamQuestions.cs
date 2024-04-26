namespace EntityLayer.Concrete
{
    public class ExamQuestions
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

    }
}

