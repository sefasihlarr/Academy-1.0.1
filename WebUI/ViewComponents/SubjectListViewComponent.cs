using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class SubjectListViewComponent : ViewComponent
    {
        SubjectManager _lessonManager = new SubjectManager(new EfCoreSubjectRepository());

        public IViewComponentResult Invoke(SubjectModel model)
        {
            var subjects = _lessonManager.GetAll();
            ViewBag.subjects = new SelectList(subjects, "Id", "Name");
            return View(model);
        }
    }
}
