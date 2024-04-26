using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class AppRoleManager : IAppRoleService
    {
        private readonly IAppRoleDal _appRoleDal;

        public AppRoleManager(IAppRoleDal appRoleDal)
        {
            _appRoleDal = appRoleDal;
        }

        public void Create(AppRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(AppRole entity)
        {
            throw new NotImplementedException();
        }

        public List<AppRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public AppRole GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AppRole> ListTogetherUser()
        {
            throw new NotImplementedException();
        }

        public void Update(AppRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
