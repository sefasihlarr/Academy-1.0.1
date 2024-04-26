using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IExamService : IGenericService<Exam>
    {
        List<Exam> GetWithList();
        void DeleteFromExam(int examId, int classId, int lessonId, int subjectId);
    }
}
