using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ISolutionService : IGenericService<Solution>
    {
        List<Solution> GetWithQuestionList();
    }
}
