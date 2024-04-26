using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class LessonListViewComponent : ViewComponent
    {
        LessonManager _lessonManager = new LessonManager(new EfCoreLessonRepository());

        public IViewComponentResult Invoke(LessonModel model)
        {
            var lesson = _lessonManager.GetAll();
            ViewBag.lessons = new SelectList(lesson, "Id", "Name");
            return View(model);
        }
    }
}
