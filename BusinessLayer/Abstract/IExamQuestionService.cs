using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IExamQuestionService : IGenericService<ExamQuestions>
    {
        void Create(ExamQuestions entity, int questionId);

        void Update(ExamQuestions entity, int[] questionIds);
        List<ExamQuestions> GetQuestionsList();
        void DeleteFormExamQuestion(ExamQuestions entity, int questionId);
    }
}
