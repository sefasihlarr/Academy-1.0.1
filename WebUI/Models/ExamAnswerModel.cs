using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class ExamAnswerModel
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int? OptionId { get; set; }
        public Option Option { get; set; }
    }
}
