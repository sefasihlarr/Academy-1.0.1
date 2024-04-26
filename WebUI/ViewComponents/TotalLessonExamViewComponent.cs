using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class TotalLessonExamViewComponent : ViewComponent
    {
        ExamManager _examManager = new ExamManager(new EfCoreExamRepository());

        public IViewComponentResult Invoke()
        {

            var values = new TotalLessonExamsModel()
            {
                TotalBiyoloji = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Biyoloji"),
                TotalFizik = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Fizik"),
                TotalKimya = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Kimya"),
                TotalFelsefe = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Felsefe"),
                TotalTukce = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Türkçe"),
                TotalEdebiyat = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Edebiyat"),
                TotalCografya = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Coğrafya"),
                TotalTarih = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Tarih"),
                TotalMatematik = _examManager.GetWithList().Count(x => x.Lesson?.Name == "Matematik")
            };

            return View(values);
        }
    }
}
