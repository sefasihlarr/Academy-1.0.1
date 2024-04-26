using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class OptionManager : IOptionService
    {
        private readonly IOptionDal _optionDal;

        public OptionManager(IOptionDal optionDal)
        {
            _optionDal = optionDal;
        }

        public void Create(Option entity)
        {
            _optionDal.Create(entity);
        }

        public void Delete(Option entity)
        {
            _optionDal.Delete(entity);
        }

        public List<Option> GetAll()
        {
            return _optionDal.GetAll().ToList();
        }

        public Option GetById(int id)
        {
            return _optionDal.GetById(id);
        }

        public void Update(Option entity)
        {
            _optionDal.Update(entity);
        }
    }
}
