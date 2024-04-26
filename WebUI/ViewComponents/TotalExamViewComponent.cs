using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class TotalExamViewComponent : ViewComponent
    {

        ExamManager _examManager = new ExamManager(new EfCoreExamRepository());

        public IViewComponentResult Invoke()
        {
            var values = new TotalCountsModel()
            {
                TotalExam = _examManager.GetAll().Count()

            };

            return View(values);
        }
    }
}
