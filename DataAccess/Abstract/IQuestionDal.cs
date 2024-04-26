using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IQuestionDal : IGenericDal<Question>
    {
        void DeleteFromQuestion(int questionId, int outputId, int optionId, int subjectId, int lessonId);
        List<Question> GetWithList();

        List<Question> GetQuestionsByExamList(int id);
    }
}
