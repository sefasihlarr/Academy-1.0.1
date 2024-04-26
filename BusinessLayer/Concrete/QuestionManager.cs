using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionDal _questionDal;

        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }

        public void Create(Question entity)
        {
            _questionDal.Create(entity);
        }

        public void Delete(Question entity)
        {
            _questionDal.Delete(entity);
        }

        public void DeleteFromQuestion(int questionId, int outputId, int optionId, int subjectId, int lessonId)
        {
            _questionDal.DeleteFromQuestion(questionId, outputId, optionId, subjectId, lessonId);
        }

        public List<Question> GetAll()
        {
            return _questionDal.GetAll().ToList();
        }

        public Question GetById(int id)
        {
            return _questionDal.GetById(id);
        }

        public List<Question> GetQuestionsByExam(int id)
        {
            return _questionDal.GetQuestionsByExamList(id).ToList();
        }

        public List<Question> GetWithList()
        {
            return _questionDal.GetWithList().ToList();
        }

        public void Update(Question entity)
        {
            _questionDal.Update(entity);
        }
    }
}
