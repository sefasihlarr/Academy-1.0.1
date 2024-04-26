using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        SolutionManager _solutinManager = new SolutionManager(new EfCoreSolutionRepository());
        QuestionManager _questionManager = new QuestionManager(new EfCoreQuestionRepository());

        public HomeController(UserManager<AppUser> userManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            var values = new TotalCountsModel()
            {
                TotalQuestion = _questionManager.GetAll().Count(),
                TotalSolution = _solutinManager.GetAll().Count()
            };

            return View(values);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}