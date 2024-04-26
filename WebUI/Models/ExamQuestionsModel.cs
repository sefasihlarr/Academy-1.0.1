using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class ExamQuestionsModel
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }


        public List<Question> SelectedQuestions { get; set; }
    }
}
