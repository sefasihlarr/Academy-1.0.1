using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IExamDal : IGenericDal<Exam>
    {
        List<Exam> GetWithList();

        void DeleteFromExam(int examId, int classId, int lessonId, int subjectId);

    }
}
