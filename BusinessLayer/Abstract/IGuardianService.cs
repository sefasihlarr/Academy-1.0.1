using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IGuardianService : IGenericService<Guardian>
    {
        List<Guardian> GetWithStudentList();
    }
}
