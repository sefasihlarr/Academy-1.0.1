using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IExamQuestionDal : IGenericDal<ExamQuestions>
    {
        void Create(ExamQuestions entity, int questionId);
        void Update(ExamQuestions entity, int[] questionIds);
        List<ExamQuestions> GetQuestionsList();
        void DeleteFromExamQuestion(ExamQuestions entity, int questionId);
    }
}
