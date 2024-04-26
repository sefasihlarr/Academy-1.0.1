using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class TotalQuestionViewComponent : ViewComponent
    {
        QuestionManager _questionManager = new QuestionManager(new EfCoreQuestionRepository());
        public IViewComponentResult Invoke()
        {
            var values = new TotalCountsModel()
            {
                TotalQuestion = _questionManager.GetAll().Count()
            };
            return View(values);
        }
    }
}
