using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFreamwork
{
    public class EfCoreSubjectRepository : EfCoreGenericRepository<Subject, AcademyContext>, ISubjectDal
    {
        //iliskili tablodan veri silme islemi
        public void DeleteFromSubject(int subjectId, int lessonId)
        {
            using (var _context = new AcademyContext())
            {
                var cmd = @"delete from Subjects where Id=@p0 And LessonId=@p1";
                _context.Database.ExecuteSqlRaw(cmd, subjectId, lessonId);
            }
        }
        //iliskili veri listeleme islemi
        public List<Subject> GetWithLessonList()
        {
            using (var _context = new AcademyContext())
            {
                return _context.Subjects.Include(x => x.Lesson).ToList();
            }
        }
        //iliskili veri guncelleme islemi
        public override void Update(Subject entity)
        {
            using (var _context = new AcademyContext())
            {
                _context.Subjects.Update(entity);
                _context.SaveChanges();
            }
        }
    }
}
