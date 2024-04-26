using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class BranchManager : IBranchService
    {
        private readonly IBranchDal _branchDal;

        public BranchManager(IBranchDal branchDal)
        {
            _branchDal = branchDal;
        }

        public void Create(Branch entity)
        {
            _branchDal.Create(entity);
        }

        public void Delete(Branch entity)
        {
            _branchDal.Delete(entity);
        }

        public List<Branch> GetAll()
        {
            return _branchDal.GetAll().ToList();
        }

        public Branch GetById(int id)
        {
            return _branchDal.GetById(id);
        }

        public void Update(Branch entity)
        {
            _branchDal.Update(entity);
        }
    }
}
