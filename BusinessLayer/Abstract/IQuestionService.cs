using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IQuestionService : IGenericService<Question>
    {
        List<Question> GetWithList();
        List<Question> GetQuestionsByExam(int id);
        void DeleteFromQuestion(int questionId, int outputId, int optionId, int subjectId, int lessonId);
    }
}
