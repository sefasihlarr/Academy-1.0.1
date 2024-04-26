using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class GuardianUpdateViewComponent:ViewComponent
    {
        GuardianManager _guardianManager = new GuardianManager(new EfCoreGuardianRepository());
        public IViewComponentResult Invoke()
        {
            var guardian  = _guardianManager.GetWithStudentList().FirstOrDefault(x=>x.Id == ViewBag.GuardianId);

            var values = new GuardianModel()
            {
                GuardianName = guardian.GuardianName,
                GuardianSurName = guardian.GuardianSurName,
                GuardianPhone = guardian.GuardianPhone,
                GuardianName2 = guardian.GuardianName2,
                GuardianSurName2 = guardian.GuardianSurName2,
                GuardianPhone2 = guardian.GuardianPhone2,
                
            };


            return View(values);
        }
    }
}
