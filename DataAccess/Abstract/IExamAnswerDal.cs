using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IExamAnswerDal : IGenericDal<ExamAnswers>
    {
        List<ExamAnswers> GetListTogether();
        void Create(ExamAnswers entity, int questionId, int? optionIds);
    }
}
