using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IExamAnswerService : IGenericService<ExamAnswers>
    {
        List<ExamAnswers> GetListTogether();
        void Create(ExamAnswers entity, int questionId, int? optionId);
    }
}
