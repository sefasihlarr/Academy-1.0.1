using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class QuestionOptionsModel
    {
        public List<Class> Classes { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Output> Outputs { get; set; }
    }
}
