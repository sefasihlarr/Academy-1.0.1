using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class GuardianManager : IGuardianService
    {
        private IGuardianDal _guardianDal;

        public GuardianManager(IGuardianDal guardianDal)
        {
            _guardianDal = guardianDal;
        }

        public void Create(Guardian entity)
        {
            _guardianDal.Create(entity);
        }

        public void Delete(Guardian entity)
        {
            _guardianDal.Delete(entity);
        }

        public List<Guardian> GetAll()
        {
            return _guardianDal.GetAll().ToList();
        }

        public Guardian GetById(int id)
        {
            return _guardianDal.GetById(id);
        }

        public List<Guardian> GetWithStudentList()
        {
            return _guardianDal.GetWithStudentList().ToList();
        }

        public void Update(Guardian entity)
        {
            _guardianDal.Update(entity);

        }
    }

}
