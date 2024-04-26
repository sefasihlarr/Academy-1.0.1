using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface ISolutionDal : IGenericDal<Solution>
    {
        List<Solution> GetWithQuestionList();
    }
}
