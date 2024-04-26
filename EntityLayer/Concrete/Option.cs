namespace EntityLayer.Concrete
{
    public class Option
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public List<ExamAnswers> ExamAnswers { get; set; }

        public Boolean Condition { get; set; }

    }
}
