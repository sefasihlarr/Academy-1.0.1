using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface ILessonDal : IGenericDal<Lesson>
    {
        void DeleteFromLesson(int lessonId, int classId);
        List<Lesson> GetWithClassList();
    }
}
