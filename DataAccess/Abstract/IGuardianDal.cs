using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IGuardianDal : IGenericDal<Guardian>
    {
        List<Guardian> GetWithStudentList();
    }
}
