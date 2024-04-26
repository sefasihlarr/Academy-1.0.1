using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IOutputDal : IGenericDal<Output>
    {
        void Delete(int outputId, int subjectId);
        List<Output> GetWithSubjectList();
    }
}
