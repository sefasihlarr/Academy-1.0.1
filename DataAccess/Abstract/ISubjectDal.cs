using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface ISubjectDal : IGenericDal<Subject>
    {
        void DeleteFromSubject(int subjectId, int lessonId);
        List<Subject> GetWithLessonList();
    }
}
