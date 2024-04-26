using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EfCoreRepository
{
    public class EfCoreGuardianRepository : EfCoreGenericRepository<Guardian, AcademyContext>, IGuardianDal
    {
        public List<Guardian> GetWithStudentList()
        {
            using(var _context = new AcademyContext())
            {
                return _context.Guardians
                    .Include(x=>x.User)
                    .ToList();
            }
        }
    }
}
