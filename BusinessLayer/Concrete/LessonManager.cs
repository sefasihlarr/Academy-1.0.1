using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class LessonManager : ILessonService
    {
        private readonly ILessonDal _lessonDal;

        public LessonManager(ILessonDal lessonDal)
        {
            _lessonDal = lessonDal;
        }

        public void Create(Lesson entity)
        {
            _lessonDal.Create(entity);
        }

        public void Delete(Lesson entity)
        {
            _lessonDal.Delete(entity);
        }

        public void DeleteFromLesson(int lessonId, int classId)
        {
            _lessonDal.DeleteFromLesson(lessonId, classId);
        }

        public List<Lesson> GetAll()
        {
            return _lessonDal.GetAll().ToList();
        }

        public Lesson GetById(int id)
        {
            return _lessonDal.GetById(id);
        }

        public List<Lesson> GetWithClassList()
        {
            return _lessonDal.GetWithClassList().ToList();
        }

        public void Update(Lesson entity)
        {
            _lessonDal.Update(entity);
        }
    }
}
