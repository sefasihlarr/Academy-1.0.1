using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class QuestionListModel
    {
        public List<Question> Questions { get; set; }
        public List<Class> Classes { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Output> Outputs { get; set; }
        public int SureDegeri { get; set; }

        public List<int> SelectedQuestions { get; set; }
    }
}
