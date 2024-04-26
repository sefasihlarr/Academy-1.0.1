using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFreamwork
{
    public class EfCoreQuestionRepository : EfCoreGenericRepository<Question, AcademyContext>, IQuestionDal
    {
        public override void Update(Question entity)
        {
            using (var _context = new AcademyContext())
            {
                _context.Questions.Update(entity);
                _context.SaveChanges();
            }
        }

        public void DeleteFromQuestion(int questionId, int outputId, int optionId, int subjectId, int lessonId)
        {
            using (var _context = new AcademyContext())
            {
                var cmd = @"delete from Question where QuestionId=@p0 And OptionId=@p1 And SubjectId=@p2 And LessonId=@p3";
                _context.Database.ExecuteSqlRaw(cmd, questionId, outputId, optionId, lessonId);
            }
        }

        public List<Question> GetWithList()
        {
            using (var _context = new AcademyContext())
            {
                return _context.Questions
                    .Include(x => x.Lesson)
                    .Include(x => x.Level)
                    .Include(x => x.Subject)
                    .Include(x => x.Options).ToList();
            }
        }

        public List<Question> GetQuestionsByExamList(int id)
        {
            using (var _context = new AcademyContext())
            {
                var questions = _context.Questions.AsQueryable();

                if (id != null)
                {
                    questions = questions
                        .Include(x => x.Lesson)
                        .Include(x => x.Level)
                        .Include(x => x.Subject)
                        .Include(x => x.Output)
                        .Include(x => x.Options)
                        .Include(x => x.ExamQuestions)
                        .ThenInclude(a => a.Exam)
                        .Where(x => x.ExamQuestions.Any(x => x.Exam.Id == id));

                }

                return questions.ToList();
            }
        }
    }
}
