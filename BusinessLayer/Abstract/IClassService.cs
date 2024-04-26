using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IClassService : IGenericService<Class>
    {
        ClassBranch GetByIdWithBrances(int id);

        List<ClassBranch> GetClassBranchList();
        void Update(Class entity, int[] branchIds);
    }
}
