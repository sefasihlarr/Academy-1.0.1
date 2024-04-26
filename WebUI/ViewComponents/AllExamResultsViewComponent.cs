using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class AllExamResultsViewComponent : ViewComponent
    {
        ScorsManager _scorsManager = new ScorsManager(new EfCoreScorsRepository());
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IViewComponentResult Invoke(int LessonId)
        {
            if (LessonId == 0)
            {
                LessonId = 4;
            }
            var values = new ScorListModel()
            {
                scors = _scorsManager.GetTogetherList().Where(x => x.Exam.Lesson.Id == ViewBag.LessonId & x.UserId == ViewBag.userId).ToList()
            };

            if (values != null)
            {
                return View(values);
            }

            return View(LessonId);

        }
    }
}
