using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EfCoreRepository
{
    public class EfCoreExamAswerRepository : EfCoreGenericRepository<ExamAnswers, AcademyContext>, IExamAnswerDal
    {
        public void Create(ExamAnswers entity, int questionId, int? optionId)
        {
            using (var _context = new AcademyContext())
            {
                entity.QuestionId = questionId;
                entity.OptionId = optionId;
                // "questionId" değeri "entity" nesnesinin "QuestionId" özelliğine atanır.
                _context.ExamAnswers.Add(entity);
                _context.SaveChanges();
            }
        }

        public List<ExamAnswers> GetListTogether()
        {
            using (var _context = new AcademyContext())
            {
                return _context.ExamAnswers
                    .Include(x => x.User)
                    .Include(x => x.Exam)
                    .Include(x => x.Option)
                    .Include(x => x.Question)
                    .ThenInclude(x => x.Options)
                    .ToList();
            }
        }
    }
}
