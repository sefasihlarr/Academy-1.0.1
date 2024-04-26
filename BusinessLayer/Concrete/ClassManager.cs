using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ClassManager : IClassService
    {
        private readonly IClassDal _classDal;

        public ClassManager(IClassDal classDal)
        {
            _classDal = classDal;
        }

        public void Create(Class entity)
        {
            _classDal.Create(entity);
        }

        public void Delete(Class entity)
        {
            _classDal.Delete(entity);
        }

        public List<Class> GetAll()
        {
            return _classDal.GetAll().ToList();
        }

        public Class GetById(int id)
        {
            return _classDal.GetById(id);
        }

        public Class GetByIdWithBrances(int id)
        {
            return _classDal.GetByIdWithBrances(id);
        }

        public List<ClassBranch> GetClassBranchList()
        {
            return _classDal.GetClassBranchList().ToList();
        }

        public void Update(Class entity)
        {
            _classDal.Update(entity);
        }

        public void Update(Class entity, int[] branchIds)
        {
            _classDal.Update(entity, branchIds);
        }

        ClassBranch IClassService.GetByIdWithBrances(int id)
        {
            throw new NotImplementedException();
        }
    }
}
