using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFreamwork
{
    public class EfCoreExamRepository : EfCoreGenericRepository<Exam, AcademyContext>, IExamDal
    {

        public override void Update(Exam entity)
        {
            using (var _context = new AcademyContext())
            {
                _context.Exams.Update(entity);
                _context.SaveChanges();
            }
        }


        public void DeleteFromExam(int examId, int classId, int lessonId, int subjectId)
        {
            using (var _context = new AcademyContext())
            {
                var cmd = @"delete from Exams where Id=@p0 And ClassId=@p1 And LessonId=@p2 And SubjectId=@p3";
                _context.Database.ExecuteSqlRaw(cmd, examId, classId, lessonId, subjectId);
            }
        }

        public List<Exam> GetWithList()
        {
            using (var _context = new AcademyContext())
            {
                return _context.Exams
                    .Include(x => x.Lesson)
                    .Include(x => x.Class)
                    .Include(x => x.Subject)
                    .ToList();
            }
        }
    }
}
