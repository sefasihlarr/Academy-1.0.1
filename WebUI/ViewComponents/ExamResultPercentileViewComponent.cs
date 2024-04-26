using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class ExamResultPercentileViewComponent : ViewComponent
    {
        ScorsManager _scorsManager = new ScorsManager(new EfCoreScorsRepository());

        public IViewComponentResult Invoke()
        {
            var TotalPercent = _scorsManager.GetAll().Where(x => x.ExamId == ViewBag.examId).ToList();


            var TotalScorCount = TotalPercent.Count();

            decimal TotalTrue = TotalPercent.Sum(x => x.True);
            decimal TotalFalse = TotalPercent.Sum(x => x.False);
            decimal TotalNull = TotalPercent.Sum(x => x.Null);
            decimal TotalScor = TotalPercent.Sum(x => x.Scor);

            var TotalQuestion = (TotalFalse + TotalNull + TotalTrue);

            var values = new ScorListModel()
            {
                //decimal ExamScor = Math.Round(totalScore / totalQuestion, 2);

                TotalFalsePercentile = Math.Round((TotalFalse / TotalQuestion) * (100), 2),
                TotalTruePercentile = Math.Round((TotalTrue / TotalQuestion) * (100), 2),
                TotalNullPercentile = Math.Round((TotalNull / TotalQuestion) * (100), 2),
                TotalScorPercentile = Math.Round((TotalScor / TotalScorCount), 2),
            };

            return View(values);

        }
    }
}
