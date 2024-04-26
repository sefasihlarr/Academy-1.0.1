using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ISubjectService : IGenericService<Subject>
    {
        List<Subject> GetWithLessonList();

        void DeleteFromSubject(int subjectId, int lessonId);
    }


}
