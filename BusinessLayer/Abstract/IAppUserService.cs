using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        List<AppUser> ListTogether();
    }
}
