using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccessLayer.EfCoreRepository
{
    public class EfCoreOptionRepository : EfCoreGenericRepository<Option, AcademyContext>, IOptionDal
    {
    }
}
