using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class ManyExamWhichClassViewComponent:ViewComponent
    {
        ExamManager _examManager = new ExamManager(new EfCoreExamRepository());

        public IViewComponentResult Invoke()
        {
            var values = new TotalCountsModel()
            {
                ManyExamWhichClass9 = _examManager.GetWithList().Where(x => x.Class.Name == "9").Count(),
                ManyExamWhichClass10 = _examManager.GetWithList().Where(x => x.Class.Name == "10").Count(),
                ManyExamWhichClass11 = _examManager.GetWithList().Where(x => x.Class.Name == "11").Count(),
                ManyExamWhichClass12 = _examManager.GetWithList().Where(x => x.Class.Name == "12").Count(),
            };

            return View(values);
        }
    }
}
