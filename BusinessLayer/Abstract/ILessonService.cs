using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ILessonService : IGenericService<Lesson>
    {
        List<Lesson> GetWithClassList();
        void DeleteFromLesson(int lessonId, int classId);
    }
}
