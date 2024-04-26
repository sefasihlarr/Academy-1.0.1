using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class ExamInfoViewComponent : ViewComponent
    {
        ExamManager _examManager = new ExamManager(new EfCoreExamRepository());
        public IViewComponentResult Invoke()
        {
            if (ViewBag.examId != null)
            {
                var values = _examManager.GetById(ViewBag.examId);

                return View(new ExamModel()
                {
                    Id = ViewBag.examId,
                    Title = values.Title,
                    Description = values.Description,
                    Class = values.Class,
                    Lesson = values.Lesson,
                    Timer = values.Timer,
                    ExamDate = values.ExamDate,
                });
            }



            return View(new ExamModel());

        }
    }
}
