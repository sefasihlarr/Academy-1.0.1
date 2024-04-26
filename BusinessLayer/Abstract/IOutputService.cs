using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IOutputService : IGenericService<Output>
    {
        List<Output> GetWithSubjectList();
        void Delete(int outputId, int subjectId);
    }
}
