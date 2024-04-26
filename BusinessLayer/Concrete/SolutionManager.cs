using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class SolutionManager : ISolutionService
    {
        private readonly ISolutionDal _solutionDal;

        public SolutionManager(ISolutionDal solutionDal)
        {
            _solutionDal = solutionDal;
        }

        public void Create(Solution entity)
        {
            _solutionDal.Create(entity);
        }

        public void Delete(Solution entity)
        {
            _solutionDal.Delete(entity);
        }

        public List<Solution> GetAll()
        {
            return _solutionDal.GetAll().ToList();
        }

        public Solution GetById(int id)
        {
            return _solutionDal.GetById(id);
        }

        public List<Solution> GetWithQuestionList()
        {
            return _solutionDal.GetWithQuestionList().ToList();
        }

        public void Update(Solution entity)
        {
            _solutionDal.Update(entity);
        }
    }
}
