using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ExamAnswerManager : IExamAnswerService
    {
        private readonly IExamAnswerDal _examAnswerDal;

        public ExamAnswerManager(IExamAnswerDal examAnswerDal)
        {
            _examAnswerDal = examAnswerDal;
        }

        public void Create(ExamAnswers entity)
        {
            _examAnswerDal.Create(entity);
        }

        public void Create(ExamAnswers entity, int questionId, int? optionIds)
        {
            _examAnswerDal.Create(entity, questionId, optionIds);
        }

        public void Delete(ExamAnswers entity)
        {
            _examAnswerDal.Delete(entity);
        }

        public List<ExamAnswers> GetAll()
        {
            return _examAnswerDal.GetAll().ToList();
        }

        public ExamAnswers GetById(int id)
        {
            return _examAnswerDal.GetById(id);
        }

        public List<ExamAnswers> GetListTogether()
        {
            return _examAnswerDal.GetListTogether().ToList();
        }

        public void Update(ExamAnswers entity)
        {
            _examAnswerDal.Update(entity);
        }
    }
}
