using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFreamwork
{
    public class EfCoreLessonRepository : EfCoreGenericRepository<Lesson, AcademyContext>, ILessonDal
    {
        //iliskili tablodan veri silme islemi
        public void DeleteFromLesson(int lessonId, int classId)
        {
            using (var _context = new AcademyContext())
            {
                var cmd = @"delete from Lessons where Id=@p0 And ClassId=@p1";
                _context.Database.ExecuteSqlRaw(cmd, lessonId, classId);
            }
        }

        //iliskili veri listeleme islemi
        public List<Lesson> GetWithClassList()
        {
            using (var _context = new AcademyContext())
            {
                return _context.Lessons
                    .Include(x => x.Class).ToList();
            }
        }

        //iliskili veri guncelleme islemi
        public override void Update(Lesson entity)
        {
            using (var _context = new AcademyContext())
            {
                _context.Lessons.Update(entity);
                _context.SaveChanges();
            }
        }

    }
}
