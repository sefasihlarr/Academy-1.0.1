using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IAppRoleService : IGenericService<AppRole>
    {
        List<AppRole> ListTogetherUser();
    }
}
